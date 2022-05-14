using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public GameEvent Event;

    public UnityEvent Response;
    
    private void OnEnable() { Event.RegisterListerner(this); }

    private void OnDisable() { Event.UnregisterListerner(this); }

    public void OnEventRaised() { Response.Invoke(); }
}
