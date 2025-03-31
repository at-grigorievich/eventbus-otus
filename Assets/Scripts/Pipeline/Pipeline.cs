using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class Pipeline
{
    [ShowInInspector]
    public List<EventTask> EventTasks = new();

    [ShowInInspector]
    private int _index = -1;
    
    public void Run()
    {
        _index++;

        if (_index >= EventTasks.Count)
        {
            Complete();
            return;
        }
        
        var task = EventTasks[_index];
        task.Run(Run);
    }

    private void Complete()
    {
        Debug.Log("compelete pipeline");
    }

    public void Reset()
    {
        _index = -1;
    }
    
    public void AddTask(EventTask task)
    {
        EventTasks.Add(task);
    }
    
}
