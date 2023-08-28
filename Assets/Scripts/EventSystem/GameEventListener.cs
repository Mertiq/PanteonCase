using UnityEngine;
using UnityEngine.Events;

public class GameEventListener : MonoBehaviour
{
    public GameEvent @event;
    public MyGameEvent response;

    private void OnEnable() => @event.RegisterListener(this);

    private void OnDisable() => @event.UnregisterListener(this);

    public void OnEventRaised(object[] args) => response.Invoke(args);
}

[System.Serializable]
public class MyGameEvent : UnityEvent<object[]>
{
}