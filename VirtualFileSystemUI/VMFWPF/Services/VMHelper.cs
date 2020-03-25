using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using VMFWPF.Model;

namespace VMFWPF.Services
{
    class VMHelper
    {

        [DllImport("./Data/FOF.dll", EntryPoint = "IsRoot")]
        public static extern bool IsRoot(IntPtr node);

        [DllImport("./Data/FOF.dll", EntryPoint = "Inited")]
        public static extern bool Inited();

        [DllImport("./Data/FOF.dll", EntryPoint = "IsFile")]
        public static extern bool IsFile(IntPtr node);

        [DllImport("./Data/FOF.dll", EntryPoint = "IsPartition")]
        public static extern bool IsPartition(IntPtr node);

        [DllImport("./Data/FOF.dll", EntryPoint = "ReleaseSinglePtr")]
        public static extern void ReleaseSinglePtr(IntPtr ptr);

        [DllImport("./Data/FOF.dll", EntryPoint = "GetVMPath")]
        public static extern IntPtr GetVMPath();

        [DllImport("./Data/FOF.dll", EntryPoint = "GetVMSize")]
        public static extern ulong GetVMSize();


        [DllImport("./Data/FOF.dll", EntryPoint = "ReleaseGropuPoitPtr")]
        public static extern void ReleaseGropuPoitPtr(IntPtr GP);

        [DllImport("./Data/FOF.dll", EntryPoint = "InitFileManagerInstance")]
        public static extern void InitFileManagerInstance();

        [DllImport("./Data/FOF.dll", EntryPoint = "ReleaseFileManagerInstance")]
        public static extern void ReleaseFileManagerInstance();

        [DllImport("./Data/FOF.dll", EntryPoint = "GetNodeByPath")]
        public static extern IntPtr GetNodeByPath(string Path, IntPtr node);

        [DllImport("./Data/FOF.dll", EntryPoint = "MakeInnerDirViaAbsolutePath")]
        public static extern IntPtr MakeInnerDirViaAbsolutePath(string AbsolutePath);

        [DllImport("./Data/FOF.dll", EntryPoint = "MakeInnerDirViaRelativePath")]
        public static extern IntPtr MakeInnerDirViaRelativePath(string RelativePath, IntPtr node);

        [DllImport("./Data/FOF.dll", EntryPoint = "GetRootNode")]
        public static extern IntPtr GetRootNode();

        [DllImport("./Data/FOF.dll", EntryPoint = "GetParentNode")]
        public static extern IntPtr GetParentNode(IntPtr node);

        [DllImport("./Data/FOF.dll", EntryPoint = "Enter")]
        public static extern IntPtr Enter(IntPtr Current);

        [DllImport("./Data/FOF.dll", EntryPoint = "Back")]
        public static extern IntPtr Back(IntPtr Current);

        [DllImport("./Data/FOF.dll", EntryPoint = "JumpTo")]
        public static extern IntPtr JumpTo(string Path, IntPtr node);

        [DllImport("./Data/FOF.dll", EntryPoint = "NewPartition")]
        public static extern bool NewPartition(string name, char index);

        [DllImport("./Data/FOF.dll", EntryPoint = "NewFloder")]
        public static extern void NewFloder(string name, IntPtr SelectedNode);

        [DllImport("./Data/FOF.dll", EntryPoint = "DleteNode")]
        public static extern void DleteNode(IntPtr SelectedNode);

        [DllImport("./Data/FOF.dll", EntryPoint = "RenameNode")]
        public static extern void RenameNode(string newName, IntPtr SelectedNode);

        [DllImport("./Data/FOF.dll", EntryPoint = "Format")]
        public static extern void Format(Setting sets);

        [DllImport("./Data/FOF.dll", EntryPoint = "OpenVM")]
        public static extern bool OpenVM(string filepath, bool withformat,Setting sets);

        [DllImport("./Data/FOF.dll", EntryPoint = "CloseVM")]
        public static extern void CloseVM();

        [DllImport("./Data/FOF.dll", EntryPoint = "NewVMFile")]
        public static extern bool CreateVM(string path,Setting sets );

        [DllImport("./Data/FOF.dll", EntryPoint = "GetCreateNewVMFileProcess")]
        public static extern int GetCreateNewVMFileProcess();

        [DllImport("./Data/FOF.dll", EntryPoint = "GetNodeType")]
        public static extern IntPtr GetNodeType(IntPtr node);

        [DllImport("./Data/FOF.dll", EntryPoint = "ConvertTime")]
        public static extern IntPtr ConvertTime(UInt64 time);

        [DllImport("./Data/FOF.dll", EntryPoint = "Search")]
        public static extern IntPtr Search(string name, IntPtr node, bool likely = false);

        [DllImport("./Data/FOF.dll", EntryPoint = "CopyFileIn")]
        public static extern void CopyFileIn(string outPathName, IntPtr Target, bool AutoRename = false);

        [DllImport("./Data/FOF.dll", EntryPoint = "CopyFileOut")]
        public static extern void CopyFileOut(string outPath, IntPtr Source, bool AutoRename = true);

        [DllImport("./Data/FOF.dll", EntryPoint = "CopyFloderIn")]
        public static extern void CopyFloderIn(string outPath, IntPtr Target, string Filter);

        [DllImport("./Data/FOF.dll", EntryPoint = "CopyFloderOut")]
        public static extern void CopyFloderOut(string outPath, IntPtr Source,bool OverWrite = false);

        [DllImport("./Data/FOF.dll", EntryPoint = "SendToInQueue")]
        public static extern void SendToInQueue(string path, bool isFile);

        [DllImport("./Data/FOF.dll", EntryPoint = "SendToOutQueue")]
        public static extern void SendToOutQueue(IntPtr node, bool isFile);

        [DllImport("./Data/FOF.dll", EntryPoint = "SubmitIn")]
        public static extern void SubmitIn(string OutBase, IntPtr ParentNode, string Filter, bool Rename);

        [DllImport("./Data/FOF.dll", EntryPoint = "SubmitOut")]
        public static extern void SubmitOut(string OutPath, IntPtr ParentNode, bool Overwrite);



        public static ObservableCollection<VMNode> Convert(IntPtr PtrFromeEnter)
        {
            GroupPoint GP = (GroupPoint)Marshal.PtrToStructure(PtrFromeEnter, typeof(GroupPoint));
            if (GP.size == 0) return new ObservableCollection<VMNode>();
            IntPtr[] ptrs = new IntPtr[GP.size];
            Marshal.Copy(GP.ptr, ptrs, 0, (int)GP.size);

            ObservableCollection<VMNode> Childrens = new ObservableCollection<VMNode>();

            for(int i = 0; i < (int)GP.size; i++)
            {
               var node = ConvertToVMNode(ptrs[i]);
                Childrens.Add(node);
            }
            ReleaseGropuPoitPtr(PtrFromeEnter);
            return Childrens;
        }

        public static VMNode ConvertToVMNode(IntPtr nodeptr)
        {
            PathNode pnode = (PathNode)Marshal.PtrToStructure(nodeptr, typeof(PathNode));
            VMNode vnode = new VMNode()
            {
                SelfPtr = nodeptr,
                Type = GetFileExtention(nodeptr, new string(pnode.info.name).Substring(0, GetStringEnd(pnode.info.name))),
                Name = new string(pnode.info.name).Substring(0, GetStringEnd(pnode.info.name)),
                Size = pnode.info.data.count,
                IsFile = IsFile(nodeptr) && !IsPartition(nodeptr) ,
                IsChildrenLoaded = false   
            };
            if(vnode.Type == "Disk")
            {
                vnode.AccessTime = new DateTime((int)pnode.info.a_time,1,1,1,1,1);
                DiskHelper.Hold(vnode);
                //Console.WriteLine((char)vnode.AccessTime.Year);
            }
            else
            {
                try { vnode.AccessTime = new DateTime((long)pnode.info.a_time); }
                catch (Exception) { }
                try { vnode.CreateTime = new DateTime((long)pnode.info.c_time); }
                catch (Exception) { }
                try { vnode.ModifyTime = new DateTime((long)pnode.info.m_time); }
                catch (Exception) { }
            }

            return vnode;
        }

        public static string GetFileExtention(IntPtr nodeptr,string filename)
        {
            if (IsRoot(nodeptr))
            { 
                return "VMDisk";
            }
            else if (VMHelper.IsPartition(nodeptr)) return "Disk";
            else if (!VMHelper.IsFile(nodeptr)) return "Floder";
            else
            {
                var index = filename.LastIndexOf(".");
                if (index == -1) return "Unknow";
                return filename.Substring(index, filename.Length - index);
            }
        }

        private static int GetStringEnd(char[] ptr)
        {
            int index = 0;
            while (ptr[index] != '\0')
            {
                index++;
            }
            return index;
        }

        public static string ConvertPtrToString(IntPtr ptr)
        {
            return Marshal.PtrToStringAnsi(ptr);
        }

    }
}
