using System.Collections;
using System.Collections.Generic;
using com.ABinder;
using UnityEngine;
using UnityEngine.UI;

[BindSource]
public class FlagsPanelView : ABindSource
{
    private Sprite _sprite;
    
    public void OnSpriteSelected(Image image)
    {
        _sprite = image.sprite;
        OnPropertyChanged();
    }
    
    [Bindable]
    public Sprite SelectedSprite
    {
        get
        {
            return _sprite;
        }
    }
}
