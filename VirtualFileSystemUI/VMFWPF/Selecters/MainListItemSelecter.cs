using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using VMFWPF.Model;
using VMFWPF.Services;

namespace VMFWPF.Selecters
{
    class MainListItemSelecter:DataTemplateSelector
    {
        public DataTemplate FileTypeTemplate { get; set; }
        public DataTemplate OtherTypeTemplate { get; set; }

        public override DataTemplate SelectTemplate(object item, DependencyObject container)
        {
            var n = item as VMNode;

            if (n != null && n.SelfPtr != IntPtr.Zero && VMHelper.IsFile(n.SelfPtr))
                return FileTypeTemplate;
            return OtherTypeTemplate;
                //base.SelectTemplate(item, container);
        }
    }
}
