using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VMFWPF.Model;

namespace VMFWPF.Converts
{
    class EqualToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!=null && parameter != null)
            {
                if(value is VMNode)
                {
                    var node = (VMNode)value;
                    if (node.Type.StartsWith((string)parameter) ) return false;
                    else return true;
                }
                if (value is bool)
                {
                    Console.WriteLine(((bool)value).ToString());
                    if (((bool)value).ToString()==(string)parameter)
                        return true;
                }

                else if (value is string)
                {
                    if ((string)value == (string)parameter) return true;
                }
                return false;
            }
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
