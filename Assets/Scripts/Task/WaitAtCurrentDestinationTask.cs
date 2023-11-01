using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class WaitAtCurrentDestinationTask : ITask
{
    public string Name => "WaitAtCurrentDestinationTask";
    public TaskPriority Priority { get; }
    public bool IsDone { get; set; }

    private NavMeshAgent _navMeshAgent;
    private float _waitTime;
    private float _startTime;

    public WaitAtCurrentDestinationTask(Character character, float waitTime, TaskPriority taskPriority = TaskPriority.Medium)
    {
        _navMeshAgent = character.GetNavMeshAgent();
        _waitTime = waitTime;
        Priority = taskPriority;
    }

    public void ExecuteStart()
    {
        IsDone = false;
        _navMeshAgent.SetDestination(_navMeshAgent.transform.position);
        _startTime = TimeManager.Instance.GetTimeSinceStartInHours();
    }

    public void ExecuteUpdate()
    {
        if (TimeManager.Instance.GetTimeSinceStartInHours() - _startTime >= _waitTime)
        {
            _navMeshAgent.ResetPath();
            IsDone = true;
        }
    }
}

