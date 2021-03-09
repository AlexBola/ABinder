using System;
using System.Reflection;
using UnityEngine;

namespace com.ABinder
{
    [BindTo(typeof(Color))]
    public class ColorBinder : ABinder
    {
        [SerializeField]
        private Component _target;
        [HideInInspector]
        public string TargetPropertyName = "";
        [HideInInspector]
        public int TargetCurrentIndex = 0;
        [HideInInspector]
        public int TargetCurrentHash = 0;

        protected override void Bind()
        {
            var value = GetSourceValue();
            //_logger.Debug(String.Format("<color=green> target: {0} property name: {1} value: {2} </color>", _target.GetType().TemplateKey, PropertyName, GetSourceValue()));
            if (_target == null)
                throw new Exception("Target reference shouldn't be null!");
            if (!IsSourceReady)
                throw new Exception("Source isn't ready to bind yet!");
            PropertyInfo propInfo = _target.GetType().GetProperty(TargetPropertyName);
            propInfo.SetValue(_target, value, null);
            //_textLabel.text = GetSourceValue() as String;
        }
    }
}
