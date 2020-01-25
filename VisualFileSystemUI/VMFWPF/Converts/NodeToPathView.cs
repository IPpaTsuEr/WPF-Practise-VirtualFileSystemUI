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
    class NodeToPathView : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && value is VMNode)
            {
                List<VMNode> pl = new List<VMNode>();
                VMNode vn = (VMNode)value;
                while (vn != null)
                {
                    pl.Insert(0,vn);
                    vn = vn.Parrent;
                }
                return pl;
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
