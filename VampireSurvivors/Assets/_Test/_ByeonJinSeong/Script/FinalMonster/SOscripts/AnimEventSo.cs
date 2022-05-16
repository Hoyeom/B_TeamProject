using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AniEvents", menuName = "SO/AnimEvent")]
public class AnimEventSo : ScriptableObject
{
    public UnityAction<FMonster, int> OnEventRaised;
    public UnityAction<AnimatorParameterSO.ParameterType> OnEventTypeNomal;
    public UnityAction<AnimatorParameterSO.ParameterType, float> OnEventTypeFlaot;
    public UnityAction<AnimatorParameterSO.ParameterType, bool> OnEventTypeBool;

    private int _hash;
    public void RaiseEvent(FMonster entity, string hesh, AnimatorParameterSO.ParameterType _Type)
    {
        _hash = Animator.StringToHash(hesh);
        OnEventTypeNomal?.Invoke(_Type);
        OnEventRaised?.Invoke(entity, _hash);
    }

    public void RaiseEvent(FMonster entity, string hesh, AnimatorParameterSO.ParameterType _Type, float value)
    {
        _hash = Animator.StringToHash(hesh);
        OnEventTypeFlaot?.Invoke(_Type, value);
        OnEventRaised?.Invoke(entity, _hash);
    }

    public void RaiseEvent(FMonster entity, string hesh, AnimatorParameterSO.ParameterType _Type, bool value)
    {
        _hash = Animator.StringToHash(hesh);
        OnEventTypeBool?.Invoke(_Type, value);
        OnEventRaised?.Invoke(entity, _hash);
    }
}