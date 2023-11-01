using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public  class MoveToPosTask : ITask
{
    public string Name => "MoveToPosTask";
    public TaskPriority Priority { get; } 
    public bool IsDone { get; set; }

    private Vector3 _targetPos;
    private NavMeshAgent _navMeshAgent;
    private float _targetTolerance;

    public MoveToPosTask(Vector3 targetPos, NavMeshAgent navMeshAgent, float targetTolerance = 0f, TaskPriority priority = TaskPriority.Medium)
    {
        _targetPos = targetPos;
        _navMeshAgent = navMeshAgent;
        _targetTolerance = targetTolerance;
        Priority = priority;
    }
    public void ExecuteStart()
    {
        IsDone = false;
        _navMeshAgent.SetDestination(_targetPos);
    }

    public void ExecuteUpdate()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance + _targetTolerance)
        {
            _navMeshAgent.ResetPath();
            IsDone = true;
        }

    }

}
