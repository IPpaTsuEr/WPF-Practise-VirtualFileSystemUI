using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VMFWPF.Model;

namespace VMFWPF.Converts
{
    class CollectionReverse : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null)
            {
                try
                {
                    var t = value as ObservableCollection<VMNode>;
                    return t.Reverse();
                }
                catch (Exception)
                {
                    return value;
                }

                //return ((PathHistory)value).History.Reverse();
            }
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
