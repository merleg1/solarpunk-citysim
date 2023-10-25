using UnityEngine;
using UnityEngine.AI;

public class Character : MonoBehaviour
{
    public float minWorkDuration;
    public float maxWorkDuration;
    public float sleepTime;
    public float sleepTimeTolerance;
    public float minSleepDuration;
    public float maxSleepDuration;
    public float hobbyDuration;
    public float eatDuration;
    public string taskName;

    private ITask _currentTask = null;
    private NavMeshAgent _navMeshAgent;

    public NavMeshAgent GetNavMeshAgent()
    {
        return _navMeshAgent;
    }

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _navMeshAgent.speed *= TimeManager.Instance.timeScale;
        _navMeshAgent.angularSpeed *= TimeManager.Instance.timeScale;
        _navMeshAgent.acceleration *= TimeManager.Instance.timeScale;
        TaskManager.Instance.AddCharacter(this);
    }

    private void Update()
    {
        if (_currentTask == null || _currentTask.IsDone)
        {
            _currentTask = TaskManager.Instance.GetNextTask(this);
            taskName = _currentTask.GetType().Name;
            _currentTask.ExecuteStart();
        }
        else
        {
            _currentTask.ExecuteUpdate();
        }
    }
}
