using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "AniEvents", menuName = "SO/AnimEvent")]
public class AnimEventSo : ScriptableObject
{
    public UnityAction OnEventRaised;

    public void RaiseEvent()
    {
        OnEventRaised?.Invoke();
    }
}
