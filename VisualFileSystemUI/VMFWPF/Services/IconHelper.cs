using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMFWPF.Services
{
    class IconHelper
    {
        public static int Level = 2;
        static Dictionary<int, string> LevelMap = new Dictionary<int, string>()
        {{0,"24x24"},{1,"32x32" },{2,"64x64" },{3,"128x128" }};

        public static string GetLevel(int level)
        {
            string part;
            LevelMap.TryGetValue(level, out part);
            return part;
        }

        public static string GetPath(string type,int level,string parameter ="")
        {
            StringBuilder str = new StringBuilder("/VMFWPF;component/Assets/Icon/");

            string part = GetLevel(level);

            str.Append(type).Append("/").Append(part).Append(parameter).Append(".png");
            Console.WriteLine(str.ToString());
            return str.ToString();
        }
        public static string GetPath(string type,string parameter="")
        {
            StringBuilder str = new StringBuilder("/VMFWPF;component/Assets/Icon/");

            string part;
            LevelMap.TryGetValue(Level, out part);

            str.Append(type).Append("/").Append(part).Append(parameter).Append(".png");
            return str.ToString();
        }
    }
}
