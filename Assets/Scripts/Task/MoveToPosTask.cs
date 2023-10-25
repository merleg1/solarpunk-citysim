using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public  class MoveToPosTask : ITask
{
    public string Name => "MoveToPosTask";
    public TaskPriority Priority => TaskPriority.Medium;
    public bool IsDone { get; set; }

    private Vector3 _targetPos;
    private NavMeshAgent _navMeshAgent;

    public MoveToPosTask(Vector3 targetPos, NavMeshAgent navMeshAgent)
    {
        _targetPos = targetPos;
        _navMeshAgent = navMeshAgent;
    }
    public void ExecuteStart()
    {
        IsDone = false;

        _navMeshAgent.SetDestination(_targetPos);
    }

    public void ExecuteUpdate()
    {
        if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            IsDone = true;
        }
    }

}