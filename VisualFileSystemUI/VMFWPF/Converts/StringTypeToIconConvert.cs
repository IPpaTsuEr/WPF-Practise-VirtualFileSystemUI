using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using VMFWPF.Model;
using VMFWPF.Services;

namespace VMFWPF.Converts
{
    class StringTypeToIconConvert : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                if(value is string)
                {
                    return "/VMFWPF;component/Assets/Icon/"+ (string)parameter+ ((string)value).Trim() + ".png";
                }
                else if(value is VMNode)
                {
                    var node = (VMNode)value;
                    if (node != null)
                    {
                        int level = 3;
                        try
                        {
                            if(parameter!=null)level = System.Convert.ToInt32(parameter);
                        }
                        catch (Exception)
                        {

                        }
                        if (!node.IsFile)
                        {
                            if(node.Type == "Floder")
                            {
                                if (node.Children == null )   return IconHelper.GetPath(node.Type, level,"New");
                                else if (node.Children.Count == 0 )   return IconHelper.GetPath(node.Type, level,"Empty");
                                else if (node.Children.Count <= 50 )   return IconHelper.GetPath(node.Type, level,"Some");
                                else return IconHelper.GetPath(node.Type, level,"Many");
                            }
                            else return IconHelper.GetPath(node.Type, level, node.Type);
                        }
                    }
                }
            }

            //return "/VMFWPF;component/Assets/Icon/" + (string)parameter + "Defalut.png";
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
