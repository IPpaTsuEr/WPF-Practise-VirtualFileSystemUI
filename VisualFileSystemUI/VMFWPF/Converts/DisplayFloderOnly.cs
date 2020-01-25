using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VMFWPF.Model;
using VMFWPF.Services;

namespace VMFWPF.Converts
{
    class DisplayFloderOnly : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
           if(value!= null)
            {
                var v = (VMNode) value;
                if (v == null) return Visibility.Collapsed;
                return  IOHelper.IsDefaulteNode(v)|| !VMHelper.IsFile(v.SelfPtr) || VMHelper.IsPartition(v.SelfPtr) ? Visibility.Visible:Visibility.Collapsed;
            }
            return Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
