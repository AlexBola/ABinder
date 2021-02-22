using System;
using UnityEngine;
using UnityEngine.UI;

namespace com.ABinder
{
    [BindTo(typeof(string))]
    public class TextBinder : ABinder
    {
        [SerializeField]
        private Text _textLabel;

        protected override void Init()
        {
            base.Init();
            if (_textLabel == null)
                _textLabel = GetComponent<Text>();
        }

        protected override void Bind()
        {
            _textLabel.text = GetSourceValue() as string;
        }
    }
}
