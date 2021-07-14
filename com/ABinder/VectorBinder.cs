using System;
using System.Reflection;
using UnityEngine;

namespace com.ABinder
{
    [BindTo(typeof(Vector3), typeof(Vector2))]
    public class VectorBinder : SimpleTypeBinder
    {}
}