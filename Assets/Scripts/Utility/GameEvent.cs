using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    protected List<GameEventListener> _listeners = new List<GameEventListener>();

    public virtual void Raise()
    {
        for (int i = _listeners.Count - 1; i >= 0; i--)
            _listeners[i].OnEventRaised();
    }

    public void Register(GameEventListener listener) { _listeners.Add(listener); }
    public void Unregister(GameEventListener listener) { _listeners.Remove(listener); }
} //Add Inspector in which we can Raise the event from a button