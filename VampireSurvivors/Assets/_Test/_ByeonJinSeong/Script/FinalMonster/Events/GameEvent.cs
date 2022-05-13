using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "GameEvent", menuName = "SO/Event")]
public class GameEvent : ScriptableObject
{
    private readonly List<SearchEventListener> eventListeners = new List<SearchEventListener>();


    public void Raise()
    {
        for(int i = eventListeners.Count-1; i >= 0; i--)
        {
            eventListeners[i].OnEventRaised();
        }
    }

    public void RegisterListerner(SearchEventListener listerner)
    {
        if (!eventListeners.Contains(listerner))
        {
            eventListeners.Add(listerner);
        }
    }

    public void UnregisterListerner(SearchEventListener listerner)
    {
        if (eventListeners.Contains(listerner))
        {
            eventListeners.Remove(listerner);
        }
    }

}
