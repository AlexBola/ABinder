using com.ABinder;

namespace Views
{
    [BindSource]
    public class RotationPanelView : ABindSource
    {
        private bool _xRotationEnabled = true;
        private bool _yRotationEnabled = true;
        private bool _zRotationEnabled = true;
        
        [Bindable]
        public bool XRotationEnabled
        {
            get
            {
                return _xRotationEnabled;
            }
            set
            {
                _xRotationEnabled = value;
                OnPropertyChanged("XRotationEnabled");
            }
        }
    
        [Bindable]
        public bool YRotationEnabled
        {
            get
            {
                return _yRotationEnabled;
            }
            set
            {
                _yRotationEnabled = value;
                OnPropertyChanged("YRotationEnabled");
            }
        }
    
        [Bindable]
        public bool ZRotationEnabled
        {
            get
            {
                return _zRotationEnabled;
            }
            set
            {
                _zRotationEnabled = value;
                OnPropertyChanged("ZRotationEnabled");
            }
        }
    }
}