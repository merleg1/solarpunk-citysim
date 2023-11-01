using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class ExploreTask : ITask
{
    public string Name => "Explore";
    public TaskPriority Priority { get; }
    public bool IsDone { get; set; }

    private NavMeshAgent _navMeshAgent;
    private Vector3 _exploreCenter;
    private float _exploreRadius;
    private float _exploreDuration;

    private float _exploreStartTime;

    public ExploreTask(NavMeshAgent agent, Vector3 center, float radius, float duration, TaskPriority taskPriority = TaskPriority.Low)
    {
        _navMeshAgent = agent;
        _exploreCenter = center;
        _exploreRadius = radius;
        _exploreDuration = duration;
        Priority = taskPriority;
    }

    public void ExecuteStart()
    {
        IsDone = false;
        _exploreStartTime = TimeManager.Instance.GetTimeSinceStartInHours();
        SetRandomDestination();
    }

    public void ExecuteUpdate()
    {
        if (TimeManager.Instance.GetTimeSinceStartInHours() - _exploreStartTime >= _exploreDuration)
        {
            _navMeshAgent.ResetPath();
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
