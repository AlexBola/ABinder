using System;
using TMPro;
using UnityEngine;

namespace com.ABinder
{
    [BindTo(typeof(String))]
    public class TextMeshProInputBinder  : ABinder
    {
        [SerializeField]
        private TMP_InputField _inputFieldLabel;

         override protected void Init()
        {
            base.Init();
            if (_inputFieldLabel == null)
                _inputFieldLabel = GetComponent<TMP_InputField>();
        }

        protected override void Bind()
        {
            _inputFieldLabel.text = GetSourceValue() as String;
        }
    }
}
