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
    private MeshRenderer _meshRenderer;
    private CapsuleCollider _capsuleCollider;
    private float _waitTime;
    private float _startTime;

    public WaitAtCurrentDestinationTask(Character character, float waitTime, TaskPriority taskPriority = TaskPriority.Medium)
    {
        _navMeshAgent = character.GetNavMeshAgent();
        _meshRenderer = character.GetMeshRenderer();
        _capsuleCollider = character.GetCapsuleCollider();
        _waitTime = waitTime;
        Priority = taskPriority;
    }

    public void ExecuteStart()
    {
        IsDone = false;
        _meshRenderer.enabled = false;
        _capsuleCollider.enabled = false;
        _navMeshAgent.enabled = false;
        _startTime = TimeManager.Instance.GetTimeSinceStartInHours();
    }

    public void ExecuteUpdate()
    {
        if (TimeManager.Instance.GetTimeSinceStartInHours() - _startTime >= _waitTime)
        {
            _meshRenderer.enabled = true;
            _capsuleCollider.enabled = true;
            _navMeshAgent.enabled = true;
            IsDone = true;
        }
    }
}

