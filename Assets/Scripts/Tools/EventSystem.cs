using System;
using System.Collections.Generic;
public static class EventSystem
{
    private static readonly Dictionary<Type, Action<GameEvent>> _eventListeners = new Dictionary<Type, Action<GameEvent>>();
    private static readonly Dictionary<Delegate, Action<GameEvent>> _eventLookups = new Dictionary<Delegate, Action<GameEvent>>();
    public static void AddEventListener<T>(Action<T> callBack) where T : GameEvent
    {
        if(!_eventLookups.ContainsKey(callBack))
        {
            Action<GameEvent> newAction = (gameEvent) => callBack((T)gameEvent);
            _eventLookups[callBack] = newAction;

            if (_eventListeners.TryGetValue(typeof(T), out Action<GameEvent> internalAction))
            {
                _eventListeners[typeof(T)] = internalAction += newAction;
            }
            else
            {
                _eventListeners[typeof(T)] = newAction;
            }  
        }
    }

    public static void RemoveEventListener<T>(Action<T> callBack)
    {
        if (_eventLookups.TryGetValue(callBack, out var action))
        {
            if (_eventListeners.TryGetValue(typeof(T), out var tempAction))
            {
                tempAction -= action;
                if (tempAction == null)
                    _eventListeners.Remove(typeof(T));
                else
                    _eventListeners[typeof(T)] = tempAction;
            }

            _eventLookups.Remove(callBack);
        }
    }

    public static void Broadcast(GameEvent gameEvent)
    {
        if (_eventListeners.TryGetValue(gameEvent.GetType(), out var action))
        {
            action.Invoke(gameEvent);
        } 
    }
}
