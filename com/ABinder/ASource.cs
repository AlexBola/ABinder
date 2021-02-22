using System;
using UnityEngine;

namespace com.ABinder
{
    public abstract class ASource : MonoBehaviour, IBindSource
    {
        [SerializeField]
        private Boolean _readyToBind;
        public event EventHandler<BindEventArgs> PropertyChanged;

        protected void MakeBindReady(Boolean rebind = true)
        {
            _readyToBind = true;
            if (rebind)
                OnPropertyChanged();
        }

        protected virtual void OnEnable()
        {
            //_logger.Debug(String.Format("<color=green> ASource.OnEnable</color>"));
            OnPropertyChanged();
        }

        protected virtual void OnDisable()
        {
            _readyToBind = false;
        }
        protected void OnPropertyChanged(String propertyName = null)
        {
            if (!_readyToBind)
                return;
            var handler = PropertyChanged;
            var bindEvent = String.IsNullOrEmpty(propertyName) ? BindEventArgs.Empty : new BindEventArgs(propertyName);
            if (handler != null) handler(this, bindEvent);
        }

        public Boolean ReadyToBind
        {
            get { return _readyToBind; }
            protected set { _readyToBind = value; }
        }
    }
}
