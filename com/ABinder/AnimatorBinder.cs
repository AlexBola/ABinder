using System;
using UnityEngine;

namespace com.ABinder
{
    [BindTo(typeof(Single), typeof(Boolean), typeof(String), typeof(Int32))]
    public class AnimatorBinder : ABinder
    {
        [SerializeField]
        private Animator _animator;

        [SerializeField] 
        private string _parameterName;
        
        [SerializeField]
        private bool _invert;
        
        override protected void Init()
        {
            base.Init();
            if (_animator == null)
                _animator = GetComponent<Animator>();
        }

        protected override void Bind()
        {
            if (!_animator.gameObject.activeInHierarchy || !_animator.gameObject.activeSelf)
            {
                return;
            }
            
            var value = GetSourceValue();
            var type = value.GetType();
            var propertyName = !string.IsNullOrEmpty(_parameterName) ? _parameterName : PropertyName;
            
            if (type == typeof (Boolean))
            {
                _animator.SetBool(propertyName, _invert ? !(Boolean)value : (Boolean)value);
            }
            else if (type == typeof(Int32))
            {
                _animator.SetInteger(propertyName, (Int32)value);
            }
            else if (type == typeof (Single))
            {
                _animator.SetFloat(propertyName, (Single)value);
            }
            else if (type == typeof (String))
            {
                _animator.SetTrigger(propertyName);
            }
        }
    }
}
