using UnityEngine;

public class FmAnimation : AnimatorParameterSO
{
    [SerializeField] private AnimEventSo EventChannel;

    private void OnEnable() 
    {
        EventChannel.OnEventTypeNomal += SetParmeter;
        EventChannel.OnEventTypeBool += SetParmeter;
        EventChannel.OnEventTypeFlaot += SetParmeter;
        EventChannel.OnEventRaised += Anims;
        
    }

    private void OnDisable() 
    {
        EventChannel.OnEventTypeNomal -= SetParmeter;
        EventChannel.OnEventTypeBool -= SetParmeter;
        EventChannel.OnEventTypeFlaot -= SetParmeter;
        EventChannel.OnEventRaised -= Anims;
    }

    public void Anims(FMonster entity ,int hesh)
    {
        switch (parameterType)
        {
            case ParameterType.Bool:
                entity._animator.SetBool(hesh, boolValue);
                break;
            // Test 일단 작성 필요 시 나중에 내용 추가
            case ParameterType.Int:
                entity._animator.SetInteger(hesh, intValue);
                break;
            case ParameterType.Float:
                entity._animator.SetFloat(hesh, floatValue);
                break;
            case ParameterType.Trigger:
                entity._animator.SetTrigger(hesh);
                break;
        }
    }
}