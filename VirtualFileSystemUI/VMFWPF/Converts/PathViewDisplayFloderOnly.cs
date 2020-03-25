using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VMFWPF.Model;
using VMFWPF.Services;

namespace VMFWPF.Converts
{
    class PathViewDisplayFloderOnly : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null)
            {
                var list = (IEnumerable<VMNode>)value;
                if (list != null)
                    return from v in list where IOHelper.IsDefaulteNode(v) || !VMHelper.IsFile(v.SelfPtr) || VMHelper.IsPartition(v.SelfPtr) select v;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
