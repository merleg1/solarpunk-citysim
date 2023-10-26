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

    public bool wentToHobbyToday { get; set; }
    public bool wentToWorkToday { get; set; }
    public float remainingWorkHoursToday { get; set; }
    private int _previousHour = -1;

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
        wentToHobbyToday = false;
        wentToWorkToday = false;
        remainingWorkHoursToday = Random.Range(minWorkDuration, maxWorkDuration);
    }

    private void Update()
    {
        int currentHour = TimeManager.Instance.CurrentTime.Hour;

        if (currentHour < _previousHour)
        {
            Debug.Log("New day");
            wentToHobbyToday = false;
            wentToWorkToday = false;
            remainingWorkHoursToday = Random.Range(minWorkDuration, maxWorkDuration);
        }

        _previousHour = currentHour;

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
