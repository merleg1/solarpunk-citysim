using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTask : ITask
{
    public string Name => "WaitTask";
    public TaskPriority Priority => TaskPriority.Medium;
    public bool IsDone { get; set; }

    private float _waitTime;
    private float _startTime;

    public WaitTask(float waitTime)
    {
        _waitTime = waitTime;
    }

    public void ExecuteStart()
    {
        IsDone = false;
        _startTime = TimeManager.Instance.GetCurrentTimeinHours();
    }

    public void ExecuteUpdate()
    {
        if (TimeManager.Instance.GetCurrentTimeinHours() - _startTime >= _waitTime)
        {
            IsDone = true;
        }
    }
}

