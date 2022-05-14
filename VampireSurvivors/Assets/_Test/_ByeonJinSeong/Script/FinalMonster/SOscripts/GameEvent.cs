using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "SO/Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<EventListener> eventListeners = new List<EventListener>();

    public void Raise()
    {
        for(int i = eventListeners.Count-1; i >= 0; i--) { eventListeners[i].OnEventRaised(); }
    }

    public void RegisterListerner(EventListener listerner)
    {
        if (!eventListeners.Contains(listerner)) { eventListeners.Add(listerner); }
    }

    public void UnregisterListerner(EventListener listerner)
    {
        if (eventListeners.Contains(listerner)) { eventListeners.Remove(listerner); }
    }
}
