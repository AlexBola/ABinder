using System;

namespace com.ABinder
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class BindToAttribute : Attribute
    {
        public Type[] BindToTypes { get; private set; }

        public BindToAttribute(params Type[] values)
        {
            BindToTypes = values;
        }
    }
}
