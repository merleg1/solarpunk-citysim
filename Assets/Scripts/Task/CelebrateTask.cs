using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CelebrateTask : ITask
{
    public string Name => "CelebrateTask";
    public TaskPriority Priority { get; }
    public bool IsDone { get; set; }

    private Character _character;
    private Vector3 _origin;
    private float _celebrationRadius;
    private NavMeshAgent _navMeshAgent;
    private float _celebrationDuration;
    private float _startTime;
    private Rigidbody _rigidbody;
    private float _hopForce = 1000f;
    private float _hopInterval = 0.5f;
    private float _nextHopTime;

    public CelebrateTask(Character character, float celebrationRadius, float celebrationDuration, TaskPriority taskPriority = TaskPriority.High)
    {
        _character = character;
        _celebrationRadius = celebrationRadius;
        _celebrationDuration = celebrationDuration;
        _navMeshAgent = character.GetNavMeshAgent();
        _rigidbody = character.GetComponent<Rigidbody>();
        Priority = taskPriority;
    }

    public void ExecuteStart()
    {
        IsDone = false;
        _origin = _character.transform.position;
        _startTime = TimeManager.Instance.GetTimeSinceStartInHours();
        _nextHopTime = _startTime + _hopInterval;
    }

    public void ExecuteUpdate()
    {
        if (TimeManager.Instance.GetTimeSinceStartInHours() - _startTime < _celebrationDuration)
        {
            Vector3 randomPosition = _origin + Random.insideUnitSphere * _celebrationRadius;

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPosition, out hit, _celebrationRadius, NavMesh.AllAreas))
            {
                _navMeshAgent.SetDestination(hit.position);

                if (Time.time >= _nextHopTime && Physics.Raycast(_character.transform.position, Vector3.down, 0.1f, _character.groundLayer))
                {
                    Hop();
                }
            }
        }
        else
        {
            IsDone = true;
        }
    }

    private void Hop()
    {
        _navMeshAgent.enabled = false;
        _rigidbody.AddForce(Vector3.up * _hopForce, ForceMode.Impulse);

        _nextHopTime = Time.time + _hopInterval;
    }
}
