using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using VMFWPF.Model;
using VMFWPF.Services;

namespace VMFWPF
{
    /// <summary>
    /// DialogWindow.xaml 的交互逻辑
    /// </summary>
    public partial class DialogWindow : Window
    {
        #region 依赖属性    

        public string States
        {
            get { return (string)GetValue(StatesProperty); }
            set { SetValue(StatesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for States.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty StatesProperty =
            DependencyProperty.Register("States", typeof(string), typeof(DialogWindow), new PropertyMetadata("取消"));




        public double Total
        {
            get { return (double)GetValue(TotalProperty); }
            set { SetValue(TotalProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Total.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TotalProperty =
            DependencyProperty.Register("Total", typeof(double), typeof(DialogWindow), new PropertyMetadata(0.0));



        public double Current
        {
            get { return (double)GetValue(CurrentProperty); }
            set { SetValue(CurrentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Current.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentProperty =
            DependencyProperty.Register("Current", typeof(double), typeof(DialogWindow), new PropertyMetadata(0.0));



        public string SourceName
        {
            get { return (string)GetValue(SourceNameProperty); }
            set { SetValue(SourceNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SourceName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SourceNameProperty =
            DependencyProperty.Register("SourceName", typeof(string), typeof(DialogWindow), new PropertyMetadata(""));




        public string TargetName
        {
            get { return (string)GetValue(TargetNameProperty); }
            set { SetValue(TargetNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for TargetName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TargetNameProperty =
            DependencyProperty.Register("TargetName", typeof(string), typeof(DialogWindow), new PropertyMetadata(""));



        public string ProcessName
        {
            get { return (string)GetValue(ProcessNameProperty); }
            set { SetValue(ProcessNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ProcessName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ProcessNameProperty =
            DependencyProperty.Register("ProcessName", typeof(string), typeof(DialogWindow), new PropertyMetadata(""));
        
        #endregion

        bool usercaancel = false;

        IEnumerable<string> InSource;
        VMNode InNode;
        string OutPath;
        IEnumerable<VMNode> OutSources;
        Setting Sets;

        BackgroundWorker bw;

        public DialogWindow()
        {
            bw = new BackgroundWorker();
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.ProgressChanged += Bw_ProgressChanged;
            bw.RunWorkerCompleted += Bw_RunWorkerCompleted;
            States = "取消";
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            Closing += DialogWindow_Closing; ;
            InitializeComponent();
            DataContext = this;
        }

        private void DialogWindow_Closing(object sender, CancelEventArgs e)
        {
            if (bw.IsBusy)
            {
                if (MessageBox.Show("是否取消任务？单击‘是’将取消当前正在进行的任务并关闭窗口，单击‘否’将继续进行当前任务", "取消当前任务", MessageBoxButton.OKCancel) == MessageBoxResult.OK)
                {
                   bw.CancelAsync();
                   e.Cancel = true;
                    usercaancel = true;
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        public void InPort(IEnumerable<string> source,VMNode node)
        {
            Total = source.Count();
            TargetName = node.Name;
            InSource = source;
            InNode = node;
            bw.DoWork += Bw_DoInportWork;
            bw.RunWorkerAsync();
        }

        public void ExPort(IEnumerable<VMNode> nodes, string outpath)
        {
            Total = nodes.Count();
            TargetName = GetShortParentPath(outpath);
            OutSources = nodes;
            OutPath = outpath;
            bw.DoWork += Bw_DoExportWork;
            bw.RunWorkerAsync();
        }

        public void CreatNew(Setting sets,string FilePath)
        {
            SourceName = "内存创建数据";
            TargetName = "指定对象";
            ProcessName = "创建文件 " + FilePath;
            Total = sets.VMSize;
            OutPath = FilePath;
            Sets = sets;
            bw.DoWork += Bw_DoCreateWork;
            bw.RunWorkerAsync();

        }

        private void Bw_DoCreateWork(object sender, DoWorkEventArgs e)
        {
            if (!VMHelper.Inited()) VMHelper.InitFileManagerInstance();
            VMHelper.CreateVM(OutPath,Sets);
            ulong Processed = 0;
            while(Processed < Sets.VMSize)
            {
                Processed = (ulong)VMHelper.GetCreateNewVMFileProcess();
                bw.ReportProgress((int)Processed);
            }
        }

        private void Bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            States = "完成";
            ProcessName = "完毕";
           
            if (usercaancel) Close();
        }

        private void Bw_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if(e.UserState is string)
            {
                var item = (string)e.UserState;
                SourceName = GetParentPath(item);
                ProcessName = item;
            }
            else if(e.UserState is VMNode)
            {
                var node = (VMNode)e.UserState;
                if (node.Parrent != null) SourceName = node.Parrent.Name;
                else SourceName = "\\"+ node.Name;
                ProcessName = node.Name;
            }
            Current = e.ProgressPercentage;
        }

        private void Bw_DoInportWork(object sender, DoWorkEventArgs e)
        {
            int index = 0;
            foreach (var item in InSource)
            {
                if (bw.CancellationPending == true) {
                    bw.ReportProgress(index, item);
                    e.Cancel = true;
                    return;
                }
                IOHelper.CopyIn(item, InNode);
                index += 1;
                bw.ReportProgress(index,item);
            }
        }

        private void Bw_DoExportWork(object sender, DoWorkEventArgs e)
        {
            int index = 0;
            foreach (var item in OutSources)
            {
                if(bw.CancellationPending == true)
                {
                    bw.ReportProgress(index, item);
                    e.Cancel = true;
                    return;
                }
                
                IOHelper.CopyOut(item, OutPath);
                index += 1;
                bw.ReportProgress(index,item);
                
            }
        }

        private string GetShortParentPath(string path)
        {
            var p = GetParentPath(path);
            if (p.Length == path.Length) return path;
            return path.Replace(p,"");
        }

        private string GetParentPath(string path)
        {
            int index = path.LastIndexOf("\\");
            if (index > 0) return path.Substring(0, index);
            return path;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            if (States == "取消")
            {
                bw.CancelAsync();
                States = "取消中...";
            }
            else if (States == "完成")
            {
                DialogResult = true;
                Close();
            }

        }



    }
}
