using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace com.ABinder
{
    [BindTo(typeof (int))]
    public class CounterBinder : ABinder
    {
        [SerializeField] private TextMeshProUGUI _textLabel;
        [SerializeField] private float _countingDuration = 1f; // time which required to lerp value to target
        [SerializeField] private float _stepDuration = 0f; // sometimes it is too quickly change text every frame
        [SerializeField] private bool _isStartFromZero; // do we need each time start counting from zero
        [SerializeField] private bool _useFormatNum; // use complex text formatting
        [SerializeField] private bool _useBalanceFormatNum; // use simple text formatting (adding commas)
        [SerializeField] private int _minValue = -1; // value below which formatting is not applied. -1 use default
        [SerializeField] private string _textMask = ""; // Ex.: "Tip: {0}"

        private long _targetValue;
        private long _oldValue;
        private long _currentValue;
        private Coroutine _currentCounting;

        private bool _isCountingProcessNeedsToBeStarted;

        protected override void Init()
        {
            base.Init();
            
            if (_textLabel == null)
            {
                _textLabel = GetComponent<TextMeshProUGUI>();
            }
        }

        protected override void Bind()
        {
            _isCountingProcessNeedsToBeStarted = false;
            _targetValue = (int)GetSourceValue();
            _oldValue = _isStartFromZero ? 0 : _currentValue;

            // Stop current counting process
            if (_currentCounting != null)
            {
                StopCoroutine(_currentCounting);
            }

            if (this.gameObject.activeInHierarchy)
            {
                _currentCounting = StartCoroutine(CountingCorotine());
            }
            else
            {
                _isCountingProcessNeedsToBeStarted = true;
            }
        }

        void OnEnable()
        {
            if (!_isCountingProcessNeedsToBeStarted)
            {
                return;
            }
            _currentCounting = StartCoroutine(CountingCorotine());
            _isCountingProcessNeedsToBeStarted = false;
        }

        private IEnumerator CountingCorotine()
        {
            var lerp = 0f;
            var currStepDuration = 0f;

            if (_countingDuration > 0f && _oldValue != _targetValue)
            {
                while (lerp < 1f)
                {
                    // lerp calculates from 0 to 1 during _countingDuration period
                    lerp += Time.deltaTime / _countingDuration;
                    var val = Mathf.Lerp(_oldValue, _targetValue, lerp);
                    _currentValue = Mathf.RoundToInt(val);

                    currStepDuration += Time.deltaTime;
                    if (currStepDuration >= _stepDuration)
                    {
                        _textLabel.SetText(FormatValue(_currentValue));
                        CurrentValue = val * 0.01f;
                        currStepDuration = 0f;
                    }
                    yield return new WaitForEndOfFrame();
                }
            }
            _currentValue = _targetValue;
            CurrentValue = _targetValue * 0.01f;
            _textLabel.SetText(FormatValue(_currentValue));
        }

        private void OnDisable()
        {
            StopAllCoroutines();
            _textLabel.SetText(FormatValue(_targetValue));
        }

        private string FormatValue(long value)
        {
            var result = value + "";

            if (_useFormatNum)
            {
                result = _minValue < 0 ? value.ToString("N2") : value.ToString("N");
            }

            if (_useBalanceFormatNum)
            {
                result = value.ToString("N2");
            }

            // Apply text mask if needed
            if (!string.IsNullOrEmpty(_textMask))
            {
                result = string.Format(_textMask, result);
            }
            return result;
        }

        public void SetCountingDuration(float value)
        {
            _countingDuration = value;
        }

         protected virtual float CurrentValue { set; get; }
    }
}
