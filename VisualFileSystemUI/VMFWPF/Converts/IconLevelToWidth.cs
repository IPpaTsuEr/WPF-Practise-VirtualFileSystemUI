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
    class IconLevelToWidth : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            int level = 0;
            try
            {
                level = System.Convert.ToInt32(parameter);
                string lstr = IconHelper.GetLevel(level);
                return System.Convert.ToInt32(lstr.Split('x')[0]);
            }
            catch (Exception) {
                return null;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
