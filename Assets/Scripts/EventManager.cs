using System;
using System.Collections.Generic;
using static GameEvents;

public class EventManager
{
    private static EventManager _instance;
    public static EventManager Instance => _instance ??= new EventManager();

    private Dictionary<Type, List<Action<IEvent>>> _eventListeners;

    private EventManager()
    {
        _eventListeners = new Dictionary<Type, List<Action<IEvent>>>();
    }

    public void AddListener<T>(Action<T> listener) where T : IEvent
    {
        Type eventType = typeof(T);

        if (!_eventListeners.ContainsKey(eventType))
        {
            _eventListeners[eventType] = new List<Action<IEvent>>();
        }

        _eventListeners[eventType].Add((e) => listener((T)e));
    }

    public void RemoveListener<T>(Action<T> listener) where T : IEvent
    {
        Type eventType = typeof(T);

        if (_eventListeners.TryGetValue(eventType, out var listeners))
        {
            listeners.RemoveAll(l => l.Target == listener.Target && l.Method == listener.Method);
        }
    }

    public void Raise<T>(T @event) where T : IEvent
    {
        Type eventType = typeof(T);

        if (_eventListeners.TryGetValue(eventType, out var listeners))
        {
            foreach (var listener in listeners)
                listener(@event);
        }
    }
}

