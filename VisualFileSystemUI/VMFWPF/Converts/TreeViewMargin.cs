using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;
using VMFWPF.Services;

namespace VMFWPF.Converts
{
    class TreeViewMargin : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if(value != null && value is TreeViewItem)
            {
                var p = VisualHelper.FindVisualParent((TreeViewItem)value, null, typeof(TreeViewItem));
                if (p != null)
                {
                    var t = (Grid)VisualHelper.FindVisualChild(p, "Margin_Grid",typeof(Grid));
                    if(t!=null)return new System.Windows.Thickness(t.Margin.Left + 6, 0, 0, 0);
                }
            }
            return new System.Windows.Thickness(0, 0, 0, 0);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
