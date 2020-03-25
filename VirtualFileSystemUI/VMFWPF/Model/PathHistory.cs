using FrameLessWindow.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VMFWPF.Model
{
    public class PathHistory:NotificationObject
    {
        
        private ObservableCollection<VMNode> _History;

        public ObservableCollection<VMNode> History
        {
            get { return _History; }
            set
            {
                if (value != _History)
                {
                    _History = value;
                    RaisePropertyChangedNotify("History");
                }
            }
        }

        private int _Index;

        public int Index
        {
            get { return _Index; }
            set
            {
                if (value != _Index)
                {
                    _Index = value;
                    RaisePropertyChangedNotify("Index");
                }
            }
        }

        public PathHistory()
        {
            History = new ObservableCollection<VMNode>();
        }

        public void Push(VMNode value)
        { 
                while (Index != 0)
                {
                    History.RemoveAt(0);
                    Index--;
                }

                History.Insert(0 ,value);
            RaisePropertyChangedNotify("History");
        }
        public VMNode Pop()
        {
            var t = History.ElementAt(0);
            History.RemoveAt(0);
            return t;

        }
        public bool CanGoForward()
        {
            if(History!=null && History.Count>0 && Index>0)return true;
            return false;
        }
        public bool CanGoBackward()
        {
            if(History!=null && History.Count>0 && Index < History.Count()-1)return true;
            return false;
        }
        public VMNode GoForward()
        {
            Index--;
            if (Index < 0) Index = 0;
            RaisePropertyChangedNotify("History");
            return History.ElementAt(Index);
        }
        public VMNode GoBackward()
        {
            Index++;
            if (Index >= History.Count) Index = History.Count - 1;
            RaisePropertyChangedNotify("History");
            return History.ElementAt(Index);
        }
        
        public void Clear()
        {
            if (_History != null) _History.Clear();
            Index = 0;
        }
    }
}
