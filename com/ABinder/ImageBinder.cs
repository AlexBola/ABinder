using System;
using UnityEngine;
using UnityEngine.UI;

namespace com.ABinder
{
    [BindTo(typeof(Sprite))]
    public class ImageBinder : ABinder
    {
        [SerializeField]
        private Image _image;
        [SerializeField]
        private bool _setNativeSize;
        [SerializeField]
        private bool _preserveAspect;

        protected override void Init()
        {
            base.Init();
            if (_image == null)
                _image = GetComponent<Image>();
        }

        protected override void Bind()
        {
            _image.sprite = GetSourceValue() as Sprite;

            if(_setNativeSize)
                _image.SetNativeSize();

            _image.preserveAspect = _preserveAspect;
        }
    }
}
