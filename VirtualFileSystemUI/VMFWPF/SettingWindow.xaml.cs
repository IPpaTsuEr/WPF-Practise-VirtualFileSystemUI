using System;
using System.Collections.Generic;
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
    /// SettingWindow.xaml 的交互逻辑
    /// </summary>
    public partial class SettingWindow : Window
    {
        Setting setting;
        
        public Setting GetSetting()
        {
            return setting;
        }

        public string OutPath
        {
            get { return (string)GetValue(OutPathProperty); }
            set { SetValue(OutPathProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OutPath.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty OutPathProperty =
            DependencyProperty.Register("OutPath", typeof(string), typeof(SettingWindow), new PropertyMetadata(""));



        public string FileName
        {
            get { return (string)GetValue(FileNameProperty); }
            set { SetValue(FileNameProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileName.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileNameProperty =
            DependencyProperty.Register("FileName", typeof(string), typeof(SettingWindow), new PropertyMetadata("新建虚拟磁盘"));


        public bool NormalModel
        {
            get { return (bool)GetValue(NormalModelProperty); }
            set { SetValue(NormalModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for PathSelected.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty NormalModelProperty =
            DependencyProperty.Register("NormalModel", typeof(bool), typeof(SettingWindow), new PropertyMetadata(true));



        public bool EnableZip
        {
            get { return (bool)GetValue(EnableZipProperty); }
            set { SetValue(EnableZipProperty, value); }
        }

        // Using a DependencyProperty as the backing store for EnableZip.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty EnableZipProperty =
            DependencyProperty.Register("EnableZip", typeof(bool), typeof(SettingWindow), new PropertyMetadata(true));



        public ulong BlockSize
        {
            get { return (ulong)GetValue(BlockSizeProperty); }
            set { SetValue(BlockSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for BlockSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BlockSizeProperty =
            DependencyProperty.Register("BlockSize", typeof(ulong), typeof(SettingWindow), new PropertyMetadata((ulong)(24 * Variable.MB)));



        public ulong SectionSize
        {
            get { return (ulong)GetValue(SectionSizeProperty); }
            set { SetValue(SectionSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SectionSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty SectionSizeProperty =
            DependencyProperty.Register("SectionSize", typeof(ulong), typeof(SettingWindow), new PropertyMetadata((ulong)(68 * Variable.MB)));



        public ulong HugeSize
        {
            get { return (ulong)GetValue(HugeSizeProperty); }
            set { SetValue(HugeSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for HugeSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty HugeSizeProperty =
            DependencyProperty.Register("HugeSize", typeof(ulong), typeof(SettingWindow), new PropertyMetadata((ulong)(128.0 * Variable.MB)));



        public ulong UseableSize
        {
            get { return (ulong)GetValue(UseableSizeProperty); }
            set { SetValue(UseableSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for UseableSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty UseableSizeProperty =
            DependencyProperty.Register("UseableSize", typeof(ulong), typeof(SettingWindow), new PropertyMetadata((ulong)0));



        public ulong FileSize
        {
            get { return (ulong)GetValue(FileSizeProperty); }
            set { SetValue(FileSizeProperty, value); }
        }

        // Using a DependencyProperty as the backing store for FileSize.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty FileSizeProperty =
            DependencyProperty.Register("FileSize", typeof(ulong), typeof(SettingWindow), new PropertyMetadata((ulong)0));




        public SettingWindow()
        {
            WindowStartupLocation = WindowStartupLocation.CenterOwner;
            InitializeComponent();
            DataContext = this;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var path = IOHelper.PickFloder("选择虚拟文件存放路径");
            if (path != null)
            {
                OutPath = path;
                var ds = System.IO.DriveInfo.GetDrives();
                foreach (var item in ds)
                {
                    if (item.Name.StartsWith(path.Substring(0, 1)))
                    {
                        UseableSize = (ulong)item.AvailableFreeSpace;
                        //FileSize = UseableSize/2;
                        
                        if (FileSize > UseableSize) FileSize = UseableSize;
                        break;
                    }
                }
            }
        }

        private void Sure_Button_Click(object sender, RoutedEventArgs e)
        {
            setting.BatchBlockSize = BlockSize;
            setting.HugeFileSize = HugeSize;
            setting.SectionSize = SectionSize;
            setting.Compressable = EnableZip;
            setting.VMSize = FileSize;
            setting.Version = Variable.FORMAT_VERSION;
            setting.Mark = Variable.FORMAT_SYMBOL;
            DialogResult = true;
           
        }
        public string GetPath()
        {
            string p = OutPath;
            if (!p.EndsWith("\\")) p += "\\";
            p += FileName;
            if (!FileName.EndsWith(".vmf"))
            {
                p+= ".vmf";
            }
            return p;
        }


        private void Cancle_Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }

        static string ileag = "<>/?\\*&^%$#@!~\"‘’';:"; 
        private void TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var stb = sender as TextBox;
            StringBuilder sb = new StringBuilder();
            foreach (var item in stb.Text)
            {
                if (ileag.IndexOf(item) < 0) sb.Append(item);
            }
            stb.Text = sb.ToString();
        }
    }
}
