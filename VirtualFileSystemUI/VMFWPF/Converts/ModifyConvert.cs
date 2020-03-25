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
    class ModifyConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                if((bool)values[0]==true && (bool)values[1]==true)
                {
                    return Visibility.Visible;
                }
            }
            catch
            {

            }
            return Visibility.Hidden;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    class SModifyConvert : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var node = value as VMNode;
                if (node.ModifyMode && node.IsSelected)
                {
                    return Visibility.Visible;
                }
            }
            catch
            {

            }
            return Visibility.Hidden;
        }


        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }


}
