using Demo.Singleton;
using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;

public class UpdateDispatcher : SingletonMono<UpdateDispatcher>
{
    private ConcurrentQueue<Action> _actions = new ConcurrentQueue<Action>();

    public void Enqueue(Action action)
    {
        _actions.Enqueue(action); 
    }

    private void Update()
    {
        while (_actions.TryDequeue(out Action action))
        {
            action.Invoke();
        }
    }
}
