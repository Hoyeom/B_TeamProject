using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventListener : MonoBehaviour
{
    public GameEvent Event_Sp;
    public UnityEvent Response_Sp;

    
    private void OnEnable() 
    {
        Event_Sp.RegisterListerner(this); 
    }

    private void OnDisable() 
    {
        Event_Sp.UnregisterListerner(this); 
    }

    public void OnEventRaised() 
    {
        Response_Sp.Invoke(); 
    }
}
