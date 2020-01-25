using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VMFWPF.Services;

namespace VMFWPF.Converts
{
    class FileTypeToColor : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value!= null && value is string)
            {
               var colors = ColorHelper.GetColor((string)value,0.03f,-0.05f);//
                switch ((string)parameter)
                {
                    case "Light":
                        return colors.Light;
                    case "Dark":
                        return colors.Deep;
                    case "LightBrush":
                        return colors.LightBrush;
                    default:
                        return colors.Normal;
                }
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
