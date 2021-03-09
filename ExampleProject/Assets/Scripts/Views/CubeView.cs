using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeView : MonoBehaviour
{
    private Material _material;

    private void Awake()
    {
        _material = GetComponent<MeshRenderer>().material;
    }

    public Color AlbedoColor
    {
        set
        {
            if (_material != null)
                _material.color = value;
        }
    }
}