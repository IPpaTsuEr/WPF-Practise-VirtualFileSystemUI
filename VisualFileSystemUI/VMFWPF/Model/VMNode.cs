using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FrameLessWindow.ViewModels;
using VMFWPF.Services;

namespace VMFWPF.Model
{
    public class VMNode:NotificationObject
    {
        private string _Name;

        public string Name
        {
            get { return _Name; }
            set
            {
                if (value != _Name)
                {
                    if (SelfPtr != IntPtr.Zero && value != null)
                    {
                        VMHelper.RenameNode(value, SelfPtr);
                    }
                    _Name = value;
                    RaisePropertyChangedNotify("Name");
                }
            }
        }

        private string _Type;

        public string Type
        {
            get { return _Type; }
            set
            {
                if (value != _Type)
                {
                    _Type = value;
                    RaisePropertyChangedNotify("Type");
                }
            }
        }

        private long _Size;

        public long Size
        {
            get { return _Size; }
            set
            {
                if (value != _Size)
                {
                    _Size = value;
                    RaisePropertyChangedNotify("Size");
                }
            }
        }

        private IntPtr _SelfPtr;

        public IntPtr SelfPtr
        {
            get { return _SelfPtr; }
            set
            {
                if (value != _SelfPtr)
                {
                    _SelfPtr = value;
                    RaisePropertyChangedNotify("SelfPtr");
                }
            }
        }

        private VMNode _Parrent;

        public VMNode Parrent
        {
            get { return _Parrent; }
            set
            {
                if (value != _Parrent)
                {
                    _Parrent = value;
                    RaisePropertyChangedNotify("Parrent");
                }
            }
        }

        private DateTime _CreateTime;

        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set
            {
                if (value != _CreateTime)
                {
                    _CreateTime = value;
                    RaisePropertyChangedNotify("CreateTime");
                }
            }
        }

        private DateTime _AccessTime;

        public DateTime AccessTime
        {
            get { return _AccessTime; }
            set
            {
                if (value != _AccessTime)
                {
                    _AccessTime = value;
                    RaisePropertyChangedNotify("AccessTime");
                }
            }
        }

        private DateTime _ModifyTime;

        public DateTime ModifyTime
        {
            get { return _ModifyTime; }
            set
            {
                if (value != _ModifyTime)
                {
                    _ModifyTime = value;
                    RaisePropertyChangedNotify("ModifyTime");
                }
            }
        }

        private ObservableCollection<VMNode> _Children;

        public ObservableCollection<VMNode> Children
        {
            get { return _Children; }
            set
            {
                if (value != _Children)
                {
                    _Children = value;
                    RaisePropertyChangedNotify("Children");
                }
            }
        }

        private bool _ModifyMode;

        public bool ModifyMode
        {
            get { return _ModifyMode; }
            set
            {
                if (value != _ModifyMode)
                {
                    _ModifyMode = value;
                    RaisePropertyChangedNotify("ModifyMode");
                }
            }
        }

        private bool _IsChildrenLoaded;

        public bool IsChildrenLoaded
        {
            get { return _IsChildrenLoaded; }
            set {
                if (value != _IsChildrenLoaded)
                {
                    _IsChildrenLoaded = value;
                    RaisePropertyChangedNotify("IsChildrenLoaded");
                }
            }
        }

        private bool _IsFile;

        public bool IsFile
        {
            get { return _IsFile; }
            set
            {
                if (value != _IsFile)
                {
                    _IsFile = value;
                    RaisePropertyChangedNotify("IsFile");
                }
            }
        }

        private bool _IsSelected;

        public bool IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (value != _IsSelected)
                {
                    if (value == false) ModifyMode = false;
                    _IsSelected = value;
                    RaisePropertyChangedNotify("IsSelected");
                }
            }
        }

    }
}
