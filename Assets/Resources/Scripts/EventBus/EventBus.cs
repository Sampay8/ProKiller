using System;
using System.Collections.Generic;
using System.Linq;

public class EventBus
{
    public static EventBus Current;

    private Dictionary<string, List<Listener>> _signals = new Dictionary<string, List<Listener>>();

    public EventBus()
    {
        if (Current == null)
            Current = this;
    }

    public void Subscribe<T>(Action<T> callBack, byte priority = 1)
    {
        string key = typeof(T).Name;
        if (_signals.ContainsKey(key))
            _signals[key].Add(new Listener(callBack, priority));
        else
            _signals.Add(key, new List<Listener>() { new Listener(callBack, priority) });

        _signals[key] = _signals[key].OrderByDescending(x => x.Priority).ToList();
    }

    public void Unsubscribe<T>(Action<T> callBack)
    {
        string key = typeof(T).Name;
        if (_signals.ContainsKey(key))
        {
            var callbackToDelete = _signals[key].FirstOrDefault(x => x.Callback.Equals(callBack));

            if (callbackToDelete != null)
                _signals[key].Remove(callbackToDelete);
        }
    }

    public void Invoke<T>(T signals)
    {
        string key = typeof(T).Name;
        if (_signals.ContainsKey(key))
        {
            foreach (var obj in _signals[key])
            {
                var call = obj.Callback as Action<T>;
                call?.Invoke(signals);
            }
        }
    }
}

public class Listener
{
    public readonly object Callback;
    public readonly byte Priority;

    public Listener(object callback, byte priority)
    {
        Priority = priority;
        Callback = callback;
    }
}
