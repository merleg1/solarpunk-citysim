using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ExploreTask : ITask
{
    public string Name => "Explore";
    public TaskPriority Priority => TaskPriority.Low;
    public bool IsDone { get; set; }

    private NavMeshAgent _navMeshAgent;
    private Vector3 _exploreCenter;
    private float _exploreRadius;
    private float _exploreDuration;

    private float _exploreStartTime;

    public ExploreTask(NavMeshAgent agent, Vector3 center, float radius, float duration)
    {
        _navMeshAgent = agent;
        _exploreCenter = center;
        _exploreRadius = radius;
        _exploreDuration = duration;
    }

    public void ExecuteStart()
    {
        IsDone = false;
        _exploreStartTime = TimeManager.Instance.GetCurrentTimeinHours();
        SetRandomDestination();
    }

    public void ExecuteUpdate()
    {
        if (TimeManager.Instance.GetCurrentTimeinHours() - _exploreStartTime >= _exploreDuration)
        {
            IsDone = true;
        }
        else if (!_navMeshAgent.pathPending && _navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance)
        {
            SetRandomDestination();
        }
    }

    private void SetRandomDestination()
    {
        Vector3 randomDirection = Random.insideUnitSphere * _exploreRadius;
        randomDirection += _exploreCenter;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, _exploreRadius, NavMesh.AllAreas);

        if (hit.hit)
        {
            _navMeshAgent.SetDestination(hit.position);
        }
    }
}
