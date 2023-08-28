using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GameEvent : ScriptableObject
{
    private List<GameEventListener> listeners = new();

    public void Raise(params object[] args)
    {
        for (var i = listeners.Count - 1; i >= 0; i--)
            listeners[i].OnEventRaised(args);
    }

    public void RegisterListener(GameEventListener listener) => listeners.Add(listener);

    public void UnregisterListener(GameEventListener listener) => listeners.Remove(listener);
}