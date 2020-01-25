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
    class StringValueConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Console.WriteLine("In :" + value.GetType() + "  " + value);
            if ((string)parameter == "FromSlider")
            {
                return value;
            }
            return ((ulong)value/Variable.MB).ToString()+"MB";
            //return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //Console.WriteLine("Out :" + value.GetType() + "  " + value);
            //if ((string)parameter == "FromSlider")
            //{
            //    Console.WriteLine("Out FromSlider:" + value.GetType() + "  " + value);
            //}

            if (value is double) return Math.Floor((double)value);
            else if (value is string)
            {
                string vstr = (string)value;
                vstr = vstr.Replace("MB", "");
                try
                {
                    return System.Convert.ToUInt64(vstr) * Variable.MB;
                }catch (Exception) {  }
                try
                {
                    return System.Convert.ToDouble(vstr) * Variable.MB;
                }catch (Exception) {  }

            }
            return value;

        }
    }
}
