using System;
using com.ABinder;
using UnityEngine;

namespace com.ABinder
{
    [BindTo(typeof(Boolean))]
    public class ParticleSystemBinder : ABinder
    {
        [SerializeField]
        private ParticleSystem _particleSystem;

        override protected void Init()
        {
            base.Init();
            if (_particleSystem == null)
                _particleSystem = GetComponent<ParticleSystem>();
        }

        protected override void Bind()
        {
            var value = (Boolean) GetSourceValue();
            if (value)
                _particleSystem.Play();
            else
                _particleSystem.Stop();
        }
    }
}
