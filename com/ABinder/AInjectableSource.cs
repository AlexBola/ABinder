using System;
using UnityEngine;

namespace com.ABinder
{
    public abstract class AInjectableSource : ReactiveViewBase, IBindSource
    {
        [SerializeField]
        private bool _readyToBind;
        public event EventHandler<BindEventArgs> PropertyChanged;

        protected void MakeBindReady(bool rebind = true)
        {
            _readyToBind = true;
            
            if (rebind)
            {
                OnPropertyChanged();
            }
        }

        protected void OnPropertyChanged(string propertyName = null)
        {
            if (!_readyToBind)
            {
                return;
            }
            var handler = PropertyChanged;
            var bindEvent = string.IsNullOrEmpty(propertyName) ? BindEventArgs.Empty : new BindEventArgs(propertyName);
            if (handler != null) handler(this, bindEvent);
        }

        public bool ReadyToBind
        {
            get
            {
                return _readyToBind;
            }
            protected set
            {
                _readyToBind = value;
            }
        }
    }
}
