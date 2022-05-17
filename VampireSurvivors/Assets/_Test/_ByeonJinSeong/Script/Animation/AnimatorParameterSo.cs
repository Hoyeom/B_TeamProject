using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AnimatorParameterSO : MonoBehaviour
{
    [HideInInspector] public enum ParameterType { Bool, Int, Float, Trigger, }

    protected string parameterName = default;
    protected ParameterType parameterType = default;

    protected bool boolValue = default;
    protected int intValue = default;
    protected float floatValue = default;

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