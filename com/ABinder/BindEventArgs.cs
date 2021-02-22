using System;

namespace com.ABinder
{
    public class BindEventArgs : EventArgs
    {
        public new static readonly BindEventArgs Empty = new BindEventArgs();
        public String PropertyName { get; private set;}
        public BindEventArgs(String propertyName = null)
        {
            PropertyName = propertyName;
        }
    }
}