using System;
using TMPro;
using UnityEngine;

namespace com.ABinder
{
    [BindTo(typeof(String))]
    public class TextMeshProUGUIBinder  : ABinder
    {
        [SerializeField]
        private TextMeshProUGUI _textLabel;

         override protected void Init()
        {
            base.Init();
            if (_textLabel == null)
                _textLabel = GetComponent<TextMeshProUGUI>();
        }

        protected override void Bind()
        {
            _textLabel.text = GetSourceValue() as String;
        }
    }
}
