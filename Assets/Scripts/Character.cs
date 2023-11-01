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

    public LayerMask groundLayer;

    private ITask _currentTask = null;
    private NavMeshAgent _navMeshAgent;
    private Rigidbody _rigidbody;
    private float _initialMass;
    private float _initialDrag;
    private float _minMass = 0.1f;

    public void FinishCurrentTask()
    {
        if (_currentTask != null)
        {
            _currentTask.IsDone = true;
        }
    }

    public NavMeshAgent GetNavMeshAgent()
    {
        return _navMeshAgent;
    }

    public void AdjustNavMeshAgentSettings()
    {
        float timeScale = TimeManager.Instance.timeScale;
        _navMeshAgent.speed *= timeScale;
        _navMeshAgent.angularSpeed *= timeScale;
        _navMeshAgent.acceleration *= timeScale;
    }

    public void AdjustRigidbodySettings()
    {
        float timeScale = TimeManager.Instance.timeScale;

        float adjustedMass = _initialMass / timeScale;
        _rigidbody.mass = Mathf.Max(adjustedMass, _minMass);
        _rigidbody.drag = _initialDrag * timeScale;
    }


    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _rigidbody = GetComponent<Rigidbody>();
        _initialMass = _rigidbody.mass;
        _initialDrag = _rigidbody.drag;

        AdjustNavMeshAgentSettings();
        AdjustRigidbodySettings();

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

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == groundLayer)
        {
            _navMeshAgent.enabled = true;
        }
    }
}

