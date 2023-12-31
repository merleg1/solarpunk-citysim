using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitTask : ITask
{
    public string Name => "WaitTask";
    public TaskPriority Priority { get; }
    public bool IsDone { get; set; }

    private float _waitTime;
    private float _startTime;

    public WaitTask(float waitTime, TaskPriority taskPriority = TaskPriority.Medium)
    {
        _waitTime = waitTime;
        Priority = taskPriority;
    }

    public void ExecuteStart()
    {
        IsDone = false;
        _startTime = TimeManager.Instance.GetTimeSinceStartInHours();
    }

    public void ExecuteUpdate()
    {
        if (TimeManager.Instance.GetTimeSinceStartInHours() - _startTime >= _waitTime)
        {
            IsDone = true;
        }
    }
}

