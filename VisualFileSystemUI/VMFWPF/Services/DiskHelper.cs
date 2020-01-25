using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VMFWPF.Model;

namespace VMFWPF.Services
{
    class DiskHelper
    {
        static List<char> diskmap = new List<char>();

        public static void Hold(VMNode node)
        {
           if( node.Type == "Disk")
            {
                char i = (char)node.AccessTime.Year;
                
                foreach (var item in diskmap)
                {
                    if (item == i) return;
                }
                diskmap.Add(i);
            }
        }

        public static char GetDisk()
        {
            char i = 'C';
            while (diskmap.Contains(i))
            {
                i++;
            }
            if (i > 'Z') return (char)0;
            diskmap.Add(i);
            return i;
        }
        public static void Reset()
        {
            diskmap.Clear();
        }

    }
}
