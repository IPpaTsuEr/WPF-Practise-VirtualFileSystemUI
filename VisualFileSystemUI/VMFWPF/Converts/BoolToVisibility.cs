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
    class BoolToVisibility : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && parameter!=null)
            {
                bool revers = false;
                var cmd = (string)parameter;
                if (cmd.StartsWith("!"))
                {
                    revers = true;
                    cmd = cmd.Substring(1);
                }
                if (revers) {
                    if ((string) value != cmd ) return Visibility.Visible;
                    else return Visibility.Hidden;
                }
                else
                {
                    if ((string)value == cmd) return Visibility.Visible;
                    else return Visibility.Hidden;
                }
            }
            return Visibility.Hidden;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && parameter != null)
            {
                if ((Visibility)value == Visibility.Visible) return true ;
                else return false;
            }
            return false;
        }
    }
}
