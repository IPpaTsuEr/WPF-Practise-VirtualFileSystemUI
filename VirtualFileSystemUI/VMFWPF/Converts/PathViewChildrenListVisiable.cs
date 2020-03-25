using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using VMFWPF.Model;

namespace VMFWPF.Converts
{
    class PathViewChildrenListVisiable : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if(values != null)
            {
                var self = (VMNode)values[0];
                var current = (VMNode)values[1];
                if (current.SelfPtr == self.SelfPtr) return Visibility.Collapsed;
                if (self.Children == null || self.Children.Count == 0) return Visibility.Collapsed;
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
