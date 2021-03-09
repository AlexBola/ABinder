using System;
using com.ABinder;
using UnityEngine;
using UnityEngine.UI;
using Slider = UnityEngine.UIElements.Slider;

[BindSource]
public class ExampleView : ABindSource
{
    private float _r;
    private float _g;
    private float _b;
    private readonly int _mult = 255;
    private bool _particlesEnabled = true;

    private void OnEnable()
    {
        MakeBindReady(true);
    }

    public void OnRedChanged(String value)
    {
        _r = FromIntToFloat(int.Parse(value));
        Recalculate();
    }

    public void OnTextChanged(String value)
    {
        Debug.Log("OnTextChanged: " + value);
    }
    public void OnGreenChanged(string value)
    {
        _g = FromIntToFloat(int.Parse(value));
        Recalculate();
    }
    
    public void OnBlueChanged(string value)
    {
        _b = FromIntToFloat(int.Parse(value));
        Recalculate();
    }
    
    public void OnColorSelected(GameObject go)
    {
        var color = go.GetComponent<Image>().color;
        _r = color.r;
        _g = color.g;
        _b = color.b;
        OnPropertyChanged();
    }

    private float FromIntToFloat(int value)
    {
        if (value < 0)
        {
            return 0f;
        }
        if (value > _mult)
        {
            value = _mult;
        }
        return (float)value /_mult;
    }

    private void Recalculate()
    {
        OnPropertyChanged();
    }

    #region Bindable

    [Bindable]
    public string Red
    {
        get
        {
            return RedNumber.ToString("N0");
        }
    }
    
    [Bindable]
    public string Green
    {
        get
        {
            return GreenNumber.ToString("N0");
        }
    }
    
    [Bindable]
    public string Blue
    {
        get
        {
            return BlueNumber.ToString("N0");
        }
    }
    
    [Bindable]
    public int RedNumber
    {
        get
        {
            return (int)(_r * _mult);
        }
    }
    
    [Bindable]
    public int GreenNumber
    {
        get
        {
            return (int)(_g * _mult);
        }
    }
    
    [Bindable]
    public int BlueNumber
    {
        get
        {
            return (int)(_b * _mult);
        }
    }
    
    [Bindable]
    public float R
    {
        get
        {
            return _r;
        }
        set
        {
            _r = value;
            Recalculate();
        }
    }

    [Bindable]
    public float G
    {
        get { return _g; }
        set
        {
            _g = value;
            Recalculate();
        }
    }

    [Bindable]
    public float B
    {
        get { return _b; }
        set
        {
            _b = value;
            Recalculate();
        }
    }

    [Bindable]
    public string Multiplication
    {
        get
        {
            return (_r * _g * _b).ToString("N2");
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
    
    [Bindable]
    public string LabeledHexCode
    {
        get
        {
            return string.Format("hex: {0}", ColorUtility.ToHtmlStringRGB(Color));
        }
    }

    [Bindable]
    public Color Color
    {
        get
        {
            return new Color(_r, _g, _b);
        }
    }
    
    [Bindable]
    public Color RedGradient
    {
        get
        {
            return new Color(_r, 0, 0);
        }
    }
    
    [Bindable]
    public Color GreenGradient
    {
        get
        {
            return new Color(0, _g, 0);
        }
    }
    
    [Bindable]
    public Color BlueGradient
    {
        get
        {
            return new Color(0, 0, _b);
        }
    }
    
    [Bindable]
    public Color InvertedColor
    {
        get
        {
            return new Color(1.0f - _r, 1.0f - _g, 1.0f -_b);
        }
    }
    
    [Bindable]
    public bool ParticlesEnabled
    {
        set
        {
            _particlesEnabled = value;
            OnPropertyChanged("ParticlesEnabled");
        }
        get
        {
            return _particlesEnabled;
        }
    }
    #endregion
}
