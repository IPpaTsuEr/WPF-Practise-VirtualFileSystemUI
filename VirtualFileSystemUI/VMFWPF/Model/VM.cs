using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VMFWPF.Model
{
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct SpaceNode
    {
        public Int64 location;
        public Int64 count;//数据大小 或是 子项个数
        public Int64 blockindex; //标记所属 batch块 或是 sequence块
    }

    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    public struct FileInfo
    {
        public UInt32 type;
        public UInt64 c_time;
        public UInt64 a_time;
        public UInt64 m_time;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 256)]
        public char[] name;
        public SpaceNode data; //若是文件 则为实际数据存储位置； 若为文件夹 则为子文件列表
        public Int32 zipedSize;

    }
    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    public struct PathNode
    {
        public FileInfo info;
        public IntPtr sub;//std::list<PathNode>*
        public IntPtr pat;//PathNode*
    }

    [StructLayout(LayoutKind.Sequential, Pack = 1, CharSet = CharSet.Ansi)]
    struct GroupPoint
    {
        public IntPtr ptr;
        public UInt64 size;
    }


    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    public struct Setting
    {
        public uint Mark;
        public float Version;
        public bool Compressable;// = true;
        public UInt64 VMSize;// = 256 MB;
        public ulong BatchBlockSize;// = 48 MB;//小文件连续存储块最大大小 
        public ulong SectionSize;// = 64 MB; //单次最大映射段大小 上限为磁盘一秒读写量
        public ulong HugeFileSize;//= 128 MB;//判定为大文件的尺寸限制，将影响存储决策
    }

    public static class Variable
    {
        public const uint FORMAT_SYMBOL = 5;
        public const float FORMAT_VERSION = 0.2333f;
        public const int KB = 1024;
        public const int MB = 1024 * KB;
        public const int GB = 1024 * MB;
    }
}
