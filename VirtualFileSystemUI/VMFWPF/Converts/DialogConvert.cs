using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace VMFWPF.Converts
{
    class DialogConvert : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            double t = (double)values[0];
            double c = (double)values[1];

            switch ((string)parameter)
            {
                case "Unprocess":
                    return "（还剩" + (t - c).ToString() + "项）";
                    break;
                case "Persent":
                    
                    return c == 0 ? "0%" : String.Format("{0:p1}",c / t);
                    break;
            }
            return null;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
