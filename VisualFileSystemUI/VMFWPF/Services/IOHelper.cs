using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using VMFWPF.Model;

namespace VMFWPF.Services
{
    public class IOHelper
    {
        public static string PickFloder(string title)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Title = title;

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileName;
            return null;
        }

        public static string PickFile(string title, CommonFileDialogFilter filter = null)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = false;
            dialog.Title = title;
            if (filter != null)
            {
                dialog.Filters.Add(filter);
            }
            else
            {
                dialog.Filters.Add(new CommonFileDialogFilter("所有文件", "*.*"));
                dialog.Filters.Add(new CommonFileDialogFilter("常见图像文件", "*.jpg;*.png;*.bmp;*.gif"));
                dialog.Filters.Add(new CommonFileDialogFilter("常见文本文件", "*.txt;*.doc;*.docx;*.pdf"));
                dialog.Filters.Add(new CommonFileDialogFilter("常见音频文件", "*.mp3;*.ogg;*.wav;*.cue"));

            }

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileName;
            return null;
        }

        public static IEnumerable<string> PickFiles(string title)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = false;
            dialog.Multiselect = true;
            dialog.Title = title;
            dialog.Filters.Add(new CommonFileDialogFilter("所有文件", "*.*"));
            dialog.Filters.Add(new CommonFileDialogFilter("常见图像文件", "*.jpg;*.png;*.bmp;*.gif"));
            dialog.Filters.Add(new CommonFileDialogFilter("常见文本文件", "*.txt;*.doc;*.docx;*.pdf"));
            dialog.Filters.Add(new CommonFileDialogFilter("常见音频文件", "*.mp3;*.ogg;*.wav;*.cue"));

            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileNames;
            return null;
        }

        public static IEnumerable<string> PickFloders(string title)
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Multiselect = true;
            dialog.Title = title;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
                return dialog.FileNames;
            return null;
        }

        public static int FileOrFloder(string path)
        {
            if (File.Exists(path)) return 1;
            else if (Directory.Exists(path)) return -1;
            else return 0;
        }

        public static void CopyIn(string[] outpath, VMNode target)
        {
            foreach (var item in outpath)
            {
                CopyIn(item,target);
            }

        }

        public static void CopyIn(string outpath, VMNode target)
        {
            switch (FileOrFloder(outpath))
            {
                case 1:
                    VMHelper.CopyFileIn(outpath,target.SelfPtr);
                    break;
                case -1:
                    //Console.WriteLine("CopyFloder Into :"+ target.Name);
                    VMHelper.CopyFloderIn(outpath,target.SelfPtr,"*");
                    break;
                default:
                    //无法判定是文件还是文件夹 或权限问题
                    break;
            }
        }

        public static void CopyOut(IEnumerable<VMNode> target)
        {
           var outpath = PickFloder("请选择输出文件夹");
            if (outpath == null || outpath == string.Empty) return;
            foreach (var item in target)
            {
                CopyOut(item,outpath);
            }

        }
        public static void CopyOut(VMNode node, string path)
        {
            if (node.IsFile)
            {
                VMHelper.CopyFileOut(path, node.SelfPtr);
            }
            else
            {
                VMHelper.CopyFloderOut(path, node.SelfPtr);
            }
        }

        public static int OpenSystem()
        {
            var vmf = PickFile("选择虚拟磁盘文件", new CommonFileDialogFilter("虚拟文件系统(.vmf)", "*.vmf"));
            if (vmf == null) return -2;
            return OpenSystem(vmf);
        }
        public static int OpenSystem(string path)
        {
            if (path == null) return -1;
            if(!VMHelper.Inited())VMHelper.InitFileManagerInstance();
            Setting sets = new Setting();
            return VMHelper.OpenVM(path, false, sets) ? 0 : 1;
        }

        public static void CloseSystem()
        {
            VMHelper.CloseVM();
            VMHelper.ReleaseFileManagerInstance();
        }

        public static VMNode GetNode(IntPtr NodePtr)
        {
            return VMHelper.ConvertToVMNode(NodePtr);
        }
        public static VMNode EnterRoot()
        {
            var RP = VMHelper.GetRootNode();
            var root = GetNode(RP);
            var childrens =  VMHelper.Convert(VMHelper.Enter(RP));
            foreach (var item in childrens)
            {
                item.Parrent = root;
            }
            root.Children = childrens;
            return root;
        }

        public static ObservableCollection<VMNode> EnterTarget(VMNode Target)
        {
            if(Target != null && Target.SelfPtr != IntPtr.Zero)
            {
                if (!VMHelper.IsFile(Target.SelfPtr))
                {
                    if (Target.IsChildrenLoaded)
                    {
                        return Target.Children;
                    }
                    else
                    {
                        Target.Children = VMHelper.Convert(VMHelper.Enter(Target.SelfPtr));
                        foreach (var item in Target.Children)
                        {
                            item.Parrent = Target;
                        }
                        Target.IsChildrenLoaded = true;
                        return Target.Children;
                    }
                }
            }
            return null;
        }
        //public static ObservableCollection<VMNode> EnterTarget(IntPtr Target)
        //{
        //    if (!VMHelper.IsFile(Target))
        //        return VMHelper.Convert(VMHelper.Enter(Target));
        //    else return null;
        //}

        public static void FreshTarget(VMNode Target)
        {
            if (!VMHelper.IsFile(Target.SelfPtr))
            {
                var ct = VMHelper.Convert(VMHelper.Enter(Target.SelfPtr));
                var t = Target.Children;
                if (t == null)
                {
                    Target.Children = ct;
                    foreach (var item in ct)
                    {
                        item.Parrent = Target;
                    }
                    return;
                }
                Target.Children = new ObservableCollection<VMNode>();
                foreach (var item in ct)
                {
                    item.Parrent = Target;
                    bool find = false;
                    foreach (var sub in t)
                    {
                        if (item.Name == sub.Name)
                        {
                            find = true;
                            item.Children = sub.Children;
                            Target.Children.Add(item);
                            break;
                        }
                    }
                    if (!find)
                    {
;                       Target.Children.Add(item);
                    }
                    
                }
            }
        }

        public static VMNode GoParent(VMNode Current)
        {
            //if (Current.ParrentPtr != IntPtr.Zero && !VMHelper.IsPartition(Current.SelfPtr))
            //    return VMHelper.Convert(VMHelper.Enter(Current.ParrentPtr));
            //else 
            if (Current.Parrent == null)
            {
                var ParentPtr = VMHelper.GetParentNode(Current.SelfPtr);
                Current.Parrent = VMHelper.ConvertToVMNode(ParentPtr);
                Current.Parrent.Children = EnterTarget(Current.Parrent);
            }
            return Current.Parrent;

        }

        public static void Delete(VMNode node)
        {
            var p = node.Parrent;
            if (node.SelfPtr != IntPtr.Zero)
            {
                VMHelper.DleteNode(node.SelfPtr);
                FreshTarget(p);
            }
        }

        public static void MakePartition(VMNode node)
        {
            if(node.SelfPtr!= IntPtr.Zero && VMHelper.IsRoot(node.SelfPtr))
            {
                char index = DiskHelper.GetDisk();
                if (index != 0)
                    VMHelper.NewPartition("新建卷", index);
                else MessageBox.Show("无法创建新分区，盘符已分配完毕","创建分区失败");
            }
        }
        public static bool MakeVMFile(string OutPath,Setting Sets)
        {
            if (!VMHelper.Inited()) VMHelper.InitFileManagerInstance();
            var result = VMHelper.CreateVM(OutPath, Sets);
            VMHelper.ReleaseFileManagerInstance();
            return result;
        }

        public static ObservableCollection<VMNode> Search(string Name,VMNode Start,bool Likely = false)
        {
            if(Start!=null && Start.SelfPtr!= IntPtr.Zero)
            {
               var ptr = VMHelper.Search(Name, Start.SelfPtr, Likely);
               return VMHelper.Convert(ptr);
            }
            return null;
        }

        public static bool InnerSourceTo(string InnerSource, string FileName, string OutPath)
        {
            try
            {
                var reflaction = Assembly.GetExecutingAssembly();
                var na = reflaction.GetName().Name + "." + InnerSource;
                var strm = reflaction.GetManifestResourceStream(na);
                byte[] data = new byte[strm.Length];
                strm.Read(data, 0, (int)strm.Length);
                if (OutPath != null && !Directory.Exists(OutPath))
                {
                    var p = Directory.CreateDirectory(OutPath).FullName;
                }
                var fs = File.OpenWrite(Path.Combine(OutPath, FileName));
                fs.Write(data, 0, data.Length);
                fs.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }

        }

        public static VMNode GetTargetByName(IEnumerable<VMNode> list,string name)
        {
            foreach (var item in list)
            {
                if (item.Name == name) return item;
            }
            return null;
        }



        public static void CreateFloder(VMNode Parent)
        {
            if (Parent == null || Parent.SelfPtr == IntPtr.Zero || Parent.Type == "Root") return;
            VMHelper.NewFloder("新建文件夹", Parent.SelfPtr);
        }

        public static bool IsDefaulteNode(VMNode node)
        {
            string type = ".FastAccess.VMSearch.VMDisk";
            if (node.SelfPtr == IntPtr.Zero && type.IndexOf(node.Type) >=0 ) return true;
            return false;
        }
    }
}
