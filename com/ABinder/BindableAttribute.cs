using System;

namespace com.ABinder
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false)]
    public sealed class BindableAttribute : Attribute
    {
    }
}
