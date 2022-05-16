using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorParameterSO : MonoBehaviour
{
    public enum ParameterType { Bool, Int, Float, Trigger, }

    public string parameterName = default;
    public ParameterType parameterType = default;

    public bool boolValue = default;
    public int intValue = default;
    public float floatValue = default;

    public void SetParmeter(AnimatorParameterSO.ParameterType _Type) { parameterType = _Type; }
    public void SetParmeter(AnimatorParameterSO.ParameterType _Type, float value)
    {
        parameterType = _Type;
        floatValue = value;
    }
    public void SetParmeter(AnimatorParameterSO.ParameterType _Type, bool value)
    {
        parameterType = _Type;
        boolValue = value;
    }
}