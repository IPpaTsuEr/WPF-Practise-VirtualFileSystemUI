using FrameLessWindow.Commands;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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
    /// VMWindow.xaml 的交互逻辑
    /// </summary>
    public partial class VMWindow : FrameLessWindow.FrameLessWindow
    {
        public WindowCommands SystemCommand {get;set;}
        public WindowCommands NodeCommand {get; set; }
        public WindowCommands ProcessCommand {get; set; }
        public PathHistory ViewStack { get; set; }
        public ObservableCollection<VMNode> IndexViewTree { get; set; }
        public ObservableCollection<VMNode> History { get; set; }

        public VMWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            if(!File.Exists("./Data/FOF.dll"))
                IOHelper.InnerSourceTo("Assets.Core.FOFx64.dll", "FOF.dll", "./Data");

            SystemCommand = new WindowCommands();
            NodeCommand = new WindowCommands();
            ProcessCommand = new WindowCommands();
            SystemCommand.Actor = new Action<object>(SystemAction);
            NodeCommand.Actor = new Action<object>(NodeAction);
            ProcessCommand.Actor = new Action<object>(ProcessAction);

            ViewStack = new PathHistory();
            IndexViewTree = new ObservableCollection<VMNode>();
            History = new ObservableCollection<VMNode>();
            Loaded += VMWindow_Loaded;

            InitializeComponent();
            OpenSuccesed = false;
            DataContext = this;
        }

        private void VMWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            
        }

        private void VMWindow_Loaded(object sender, RoutedEventArgs e)
        {
            //VMNode F = new VMNode()
            //{
            //    Name = "快速访问",
            //    Type = "FastAccess",
            //    Children = new ObservableCollection<VMNode>()
            //};
            //F.Children.Add(new VMNode()
            //{
            //    Name = "快速访问AAA",
            //    Type = "FastAccess"
            //});
            //IndexViewTree.Add(F);
            //F = new VMNode()
            //{
            //    Name = "搜索",
            //    Type = "VMSearch",
            //    Children = new ObservableCollection<VMNode>()
            //};
            //IndexViewTree.Add(F);
            //F = new VMNode()
            //{
            //    Name = "磁盘",
            //    Type = "VMDisk",
            //    Children = new ObservableCollection<VMNode>()
            //};
            //IndexViewTree.Add(F);

        }

        private void GoToTarget(object sender,RoutedEventArgs e)
        {
            var t = e.OriginalSource as VMNode;
            if(t!=null)CurrentNode = t;
            //Console.WriteLine(e.GetType());
        }
        private void UpdateHistory(VMNode node)
        {
            History.Remove(node);
            History.Insert(0, node);
        }

        #region Command响应函数

        private void ProcessAction(object obj)
        {

            if (obj is ListView)
            {
                var t = (ScrollViewer)VisualHelper.FindVisualChild((ListView)obj,null,typeof(ScrollViewer));
                t.HorizontalScrollBarVisibility = ScrollBarVisibility.Hidden;
                t.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
                t.CanContentScroll = true;//使路径栏能够自动选择不显示的条目而不是像素（目前只试出配合StackPanel有效果，WrapPanel仍旧按像素）
                t.HorizontalAlignment = HorizontalAlignment.Stretch;
                t.ScrollChanged += T_ScrollChanged;
            }
        }
        //确保路径栏能够总是显示最后一个路径
        private void T_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var SV = (ScrollViewer)sender;
            SV.ScrollToHorizontalOffset(SV.ScrollableWidth);

        }

        public void SystemAction(object obj)
        {
            //Console.WriteLine((string)obj);
            switch ((string)obj)
            {
                case "Open":
                    var openresult = IOHelper.OpenSystem();
                    if (openresult == 1)
                    {
                        OpenSuccesed = false;
                        MessageBox.Show("打开文件失败！不是有效的虚拟文件系统文件。");
                    }
                    else if (openresult == -1)
                    {
                        OpenSuccesed = false;
                        MessageBox.Show("打开文件失败！");
                    }
                    else if (openresult == -2) {
                        // 用户取消
                    }
                    else
                    {
                        OpenSuccesed = true;
                        CurrentNode = IOHelper.GetNode(VMHelper.GetRootNode());
                        ViewStack.Push(CurrentNode);
                        CurrentNode.Children = IOHelper.EnterTarget(CurrentNode);
                        IndexViewTree.Add(CurrentNode);
                        UpdateHistory(CurrentNode);
                    }

                    break;
                case "Close":
                    if (OpenSuccesed)
                    {
                        OpenSuccesed = false;
                        IOHelper.CloseSystem();
                        ViewStack.Clear();
                        IndexViewTree.Clear();
                        if (CurrentNode.Children!=null) CurrentNode.Children.Clear();
                        CurrentNode = null;
                        History = new ObservableCollection<VMNode>();
                        DiskHelper.Reset();
                    }
                    break;
                case "Exite":
                    if (OpenSuccesed)
                    {
                        SystemAction("Close");
                        //OpenSuccesed = false;
                        //IOHelper.CloseSystem();
                    }
                    Close();
                    break;
                case "Format":
                    if (OpenSuccesed)
                    {
                        SettingWindow sw = new SettingWindow();
                        sw.Owner = this;
                        sw.NormalModel = false;
                        var ptr =VMHelper.GetVMPath();
                        
                        sw.OutPath = VMHelper.ConvertPtrToString(ptr);
                        sw.FileSize = VMHelper.GetVMSize();
                        sw.ShowDialog();
                        if (sw.DialogResult == true)
                        {
                            var sed = sw.GetSetting();
                            var pa = sw.GetPath();
                            VMHelper.Format(sed);
                            SystemAction("Close");
                            NodeAction(new Object[] { "Open", pa });
                        }
                        
                    }
                    break;
                case "CreateNew":
                    SettingWindow setting = new SettingWindow();
                    setting.Owner = this;
                    setting.ShowDialog();
                    if (setting.DialogResult == true)
                    {
                        var sed = setting.GetSetting();
                        var pa = setting.GetPath();
                        if(IOHelper.MakeVMFile(pa, sed))
                        {
                            NodeAction(new Object[]{"Open",pa });
                        }
                    }

                    break;
                    
                default:
                    break;
            }
        }
        public void NodeAction(object obj)
        {

            if (obj is string)
            {
                switch ((string)obj)
                {
                    case "GoBack":
                        if (ViewStack.CanGoBackward())
                        {
                            var bn = ViewStack.GoBackward();
                            //source = IOHelper.EnterTarget(bn);
                            CurrentNode = bn;
                            UpdateHistory(bn);
                        }
                        break;
                    case "GoForward":
                        if (ViewStack.CanGoForward())
                        {
                            var fn = ViewStack.GoForward();
                            //source = IOHelper.EnterTarget(fn);
                            CurrentNode  = fn;
                            UpdateHistory(fn);
                        }

                        break;
                    case "GoUp":
                        if(CurrentNode!=null && CurrentNode.Parrent != null)
                        {
                            ViewStack.Push(CurrentNode);
                            CurrentNode = IOHelper.GoParent(CurrentNode);
                            UpdateHistory(CurrentNode);
                        }

                        break;
                    case "NewFloder":
                        if (CurrentNode != null &&  CurrentNode.SelfPtr != IntPtr.Zero)
                        {
                            IOHelper.CreateFloder(CurrentNode);
                            IOHelper.FreshTarget(CurrentNode);
                        }
                        break;
                    case "NewPartition":
                        if (CurrentNode != null && CurrentNode.Type == "VMDisk")
                        {
                            IOHelper.MakePartition(CurrentNode);
                            IOHelper.FreshTarget(CurrentNode);
                        }
                        break;
                    case "Refresh":
                        if(CurrentNode != null && CurrentNode.SelfPtr!= IntPtr.Zero)
                        {
                            IOHelper.FreshTarget(CurrentNode);
                        }
                        break;
                    case "Inport":
                        if(OpenSuccesed && CurrentNode.SelfPtr != IntPtr.Zero)
                        {
                            if (CurrentNode.Type == "VMDisk" || VMHelper.IsFile(CurrentNode.SelfPtr)) return;
                            var sources = IOHelper.PickFiles("选择需要导入的文件或文件夹");
                            if (sources != null && sources.Count() > 0)
                            {
                                DialogWindow idw = new DialogWindow();
                                idw.Owner = this;
                                idw.InPort(sources, CurrentNode);
                                idw.ShowDialog();
                                IOHelper.FreshTarget(CurrentNode);
                            }
                        }
                        break;
                    case "InportFloders":
                        if(OpenSuccesed && CurrentNode.SelfPtr != IntPtr.Zero)
                        {
                            if (CurrentNode.Type == "VMDisk" || VMHelper.IsFile(CurrentNode.SelfPtr)) return;
                            var sources = IOHelper.PickFloders("选择需要导入的文件或文件夹");
                            if (sources != null && sources.Count() > 0)
                            {
                                DialogWindow idw = new DialogWindow();
                                idw.Owner = this;
                                idw.InPort(sources, CurrentNode);
                                idw.ShowDialog();
                                IOHelper.FreshTarget(CurrentNode);
                            }
                        }
                        break;
                    case "Delete":
                        IOHelper.Delete(CurrentNode);
                        break;

                    default:
                        break;
                }
            }
            else
            {
                var cmd = obj as object[];
                VMNode cn = cmd[1] as VMNode;

                switch ((string)cmd[0])
                {
                    case "Open":
                        var path = cmd[1] as string;
                        if (path == null) return;
                        int osl = IOHelper.OpenSystem(path);
                        if (osl == 1)
                        {
                            OpenSuccesed = false;
                            MessageBox.Show("打开文件失败！不是有效的虚拟文件系统文件。");
                        }
                        else if (osl == -1)
                        {
                            OpenSuccesed = false;
                            MessageBox.Show("打开文件失败！");
                        }
                        else
                        {
                            OpenSuccesed = true;
                            CurrentNode = IOHelper.GetNode(VMHelper.GetRootNode());
                            ViewStack.Push(CurrentNode);
                            CurrentNode.Children = IOHelper.EnterTarget(CurrentNode);
                            IndexViewTree.Add(CurrentNode);
                            UpdateHistory(CurrentNode);
                        }
                        break;
                    case "Enter":
                        if (cn.SelfPtr != null && !cn.IsFile)
                        {
                            cn.Parrent = CurrentNode;
                            ViewStack.Push(cn);
                            IOHelper.EnterTarget(cn);
                            CurrentNode = cn;
                            IndexViewTree.Add(new VMNode());
                            IndexViewTree.Remove(IndexViewTree.Last());
                            UpdateHistory(CurrentNode);
                        }
                        break;

                    case "Goto":
                        ViewStack.Push(cn);
                        IOHelper.EnterTarget(cn);
                        CurrentNode = cn;
                        UpdateHistory(CurrentNode);
                        break;
                    case "Rename":
                        if (cn.ModifyMode) {
                            cn.ModifyMode = false;
                            VMHelper.RenameNode(cn.Name, cn.SelfPtr);
                        }
                        else
                        {
                            cn.ModifyMode = true;
                        }
                        break;
                    case "Export":
                        IEnumerable<VMNode> nodex;
                        if (cmd[1] is VMNode)
                        {
                            var a = new List<VMNode>();
                            a.Add((VMNode)cmd[1]);
                            nodex = a;
                        }
                        else if (cmd[1] is IEnumerable)
                            nodex = ((IList)cmd[1]).Cast<VMNode>();
                        else return;

                        if (nodex.Count() > 0)
                        {
                            var outparh = IOHelper.PickFloder("请选择导出目标文件夹");
                            if (outparh != null)
                            {
                                DialogWindow dw = new DialogWindow();
                                dw.Owner = this;
                                dw.ExPort(nodex, outparh);
                                dw.ShowDialog();
                            }
                        }
                        //IOHelper.CopyOut(nodex);
                        break;
                    case "Inport":
                        if (OpenSuccesed && cn.SelfPtr != IntPtr.Zero)
                        {
                            if (cn.Type == "VMDisk" || VMHelper.IsFile(cn.SelfPtr)) return;
                            var sources = IOHelper.PickFiles("选择需要导入的文件");
                            if (sources != null && sources.Count() > 0)
                            {
                                DialogWindow idw = new DialogWindow();
                                idw.Owner = this;
                                idw.InPort(sources,cn);
                                idw.ShowDialog();
                                IOHelper.FreshTarget(cn);
                            }
                        }
                        break;
                    case "InportFloders":
                        if (OpenSuccesed && cn.SelfPtr != IntPtr.Zero)
                        {
                            if (cn.Type == "VMDisk" || VMHelper.IsFile(cn.SelfPtr)) return;
                            var sources = IOHelper.PickFloders("选择需要导入的文件夹");
                            if (sources != null && sources.Count() > 0)
                            {
                                DialogWindow idw = new DialogWindow();
                                idw.Owner = this;
                                idw.InPort(sources,cn);
                                idw.ShowDialog();
                                IOHelper.FreshTarget(cn);
                            }
                        }
                        break;
                    case "Delete":
                        IOHelper.Delete(cn);
                        break;
                    case "NewPartition":
                        if (cn.Type == "Root")
                        {
                            IOHelper.MakePartition(cn);
                        }
                        break;
                    case "PreLoad":
                        if(cn!=null && cn.SelfPtr!= IntPtr.Zero)
                            IOHelper.EnterTarget(cn);
                        break;
                    case "SideSelected":
                        if (!cn.IsChildrenLoaded) IOHelper.EnterTarget(cn);
                        CurrentNode = cn;
                        UpdateHistory(CurrentNode);
                        break;
                    case "Search":
                        if (!OpenSuccesed || CurrentNode == null || CurrentNode.SelfPtr==IntPtr.Zero) return;
                        var t = IOHelper.GetTargetByName(IndexViewTree,"搜索");
                        if (t == null)
                        {
                            t = new VMNode()
                            {
                                Name = "搜索",
                                Type = "VMSearch",
                                Children = new ObservableCollection<VMNode>()
                            };
                            IndexViewTree.Insert(0, t);
                        }
                        if (t.Children.Count > 0)
                        {
                            string title = "搜索\"" + CurrentNode.Name + "\"下\"" + (string)cmd[1] + "\"的结果";
                            foreach (var item in t.Children)
                            {
                                if(item.Name == title)
                                {
                                    t.Children.Remove(item);
                                    break;
                                }
                            }
                        }
                        var searchnode = new VMNode()
                        {
                            Name = "搜索\"" + CurrentNode.Name + "\"下\"" + (string)cmd[1] + "\"的结果",
                            Type = "VMSearch",
                            Children = IOHelper.Search((string)cmd[1], CurrentNode, true)
                        };
                        t.Children.Add(searchnode);
                        CurrentNode = searchnode;
                        UpdateHistory(searchnode);
                        break;
                    default:
                        Console.WriteLine((string)cmd[0]);
                        break;
                }
            }

        }

        #endregion

        #region 依赖属性


        public VMNode CurrentNode
        {
            get { return (VMNode)GetValue(CurrentNodeProperty); }
            set { SetValue(CurrentNodeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentNodeProperty =
            DependencyProperty.Register("CurrentNode", typeof(VMNode), typeof(VMWindow), new PropertyMetadata(null));


        public VMNode IndexTree
        {
            get { return (VMNode)GetValue(IndexTreeProperty); }
            set { SetValue(IndexTreeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IndexTree.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IndexTreeProperty =
            DependencyProperty.Register("IndexTree", typeof(VMNode), typeof(VMWindow), new PropertyMetadata(new VMNode()));



        public bool OpenSuccesed
        {
            get { return (bool)GetValue(OpenSuccesedProperty); }
            set { SetValue(OpenSuccesedProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenSuccesed.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OpenSuccesedProperty =
            DependencyProperty.Register("OpenSuccesed", typeof(bool), typeof(VMWindow), new PropertyMetadata(false));



        #endregion


        public void Main_List_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop, false))
            {
                string[] filePaths = e.Data.GetData(DataFormats.FileDrop) as string[];

                if (filePaths != null && filePaths.Count()>0 && OpenSuccesed)
                {
                    DialogWindow idw = new DialogWindow();
                    idw.Owner = this;
                    idw.InPort(filePaths, CurrentNode);
                    idw.ShowDialog();
                    IOHelper.FreshTarget(CurrentNode);
                    //IOHelper.CopyIn(,);
                }
            }
        }

        public void History_List_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //Console.WriteLine("==============================================");
            var t = sender as ListBox;
            if(t!=null && t.Parent != null)
            {
                var p = t.Parent as Popup;
                p.IsOpen = false;
            }
            var c = (VMNode)e.AddedItems[0];
            CurrentNode = c;
        }

    }
}
