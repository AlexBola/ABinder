using System;
using UnityEngine;
using UnityEngine.UI;

namespace com.ABinder
{
    [BindTo(typeof(Color))]
    public class ColorBinder : ABinder
    {
        [SerializeField]
        private Image _image;

        protected override void Init()
        {
            base.Init();
            if (_image == null)
                _image = GetComponent<Image>();
        }

        protected override void Bind()
        {
            _image.color = (Color)GetSourceValue();
        }
    }
}
