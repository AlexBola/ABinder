using com.ABinder;
using UnityEngine;

namespace com.ABinder
{
    [BindTo(typeof(int))]
    public class CounterBinderExtended : CounterBinder
    {
        /// <summary>
        ///  Fields to use counter current value to implements progressbar
        /// </summary>
        [SerializeField]
        private     RectTransform   TargetTransform;
        [SerializeField]
        private     bool            _width;     // Set it check when need to width sizing.
        [SerializeField]
        private     bool            _height;    // Set it check when need to height sizing.


        private float _value;
        private Vector2 _initialSize;

        public void Awake()
        {
            _initialSize = TargetTransform.sizeDelta;
        }

        private float Value
        {
            set
            {
                float max = 1, min = 0;
                var newValue = value;
                if (value > max)
                {
                    newValue = max;
                }
                else if (value < min)
                {
                    newValue = min;
                }
                _value = newValue;
                Resize();
            }
            get { return _value; }
        }

        private void Resize()
        {
            if (TargetTransform == null)
                return;
            var x = _width ? _initialSize.x * Value : _initialSize.x;
            var y = _height ? _initialSize.y * Value : _initialSize.y;
            TargetTransform.sizeDelta = new Vector2(x, y);
        }

        protected override float CurrentValue
        {
            set { Value = value; }
            get { return Value; }
        }
    }
}
