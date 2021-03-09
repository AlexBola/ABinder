using com.ABinder;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    [BindSource]
    public class ColorFieldView : ABindSource
    {
        [SerializeField]
        private Image _image;
        
        [Bindable]
        public Color Color
        {
            get
            {
                return _image.color;
            }
        }
        
        [Bindable]
        public string HexCode
        {
            get
            {
                return ColorUtility.ToHtmlStringRGB(Color);
            }
        }
    }
}