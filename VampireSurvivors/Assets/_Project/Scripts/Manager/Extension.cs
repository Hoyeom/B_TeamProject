using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public static class Extension
{
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        if (!go.TryGetComponent<T>(out T component))
            component = go.AddComponent<T>();

        return component;
    }
}