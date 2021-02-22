using System;

namespace com.ABinder
{
    public interface IBindSource
    {
        event EventHandler<BindEventArgs> PropertyChanged;
        Boolean ReadyToBind { get; }
    }
}
