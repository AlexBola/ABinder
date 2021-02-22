using System;
using UnityEngine;

namespace com.ABinder
{
    [BindTo(typeof(Boolean))]
    public class GameObjectBooleanBinder : ABinder
    {
        [SerializeField]
        private GameObject  _target;
        [SerializeField]
        private bool        _invert;
        override protected void Init()
        {
            base.Init();
            if (_target == null)
                _target = gameObject;
        }

        protected override void Bind()
        {
            if (_invert)
                _target.SetActive(!(Boolean)GetSourceValue());
            else
                _target.SetActive((Boolean)GetSourceValue());
        }
    }
}
