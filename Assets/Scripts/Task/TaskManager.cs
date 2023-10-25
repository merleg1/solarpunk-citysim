using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    private readonly Dictionary<Character, List<ITask>> _characterTasks = new Dictionary<Character, List<ITask>>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void AddTask(Character character, ITask task)
    {
        if (!_characterTasks.ContainsKey(character))
        {
            _characterTasks.Add(character, new List<ITask>());
        }

        _characterTasks[character].Add(task);
    }

    public void RemoveTask(Character character, ITask task)
    {
        if (!_characterTasks.ContainsKey(character))
        {
            return;
        }

        _characterTasks[character].Remove(task);
    }

    public void AddCharacter(Character character)
    {
        if (!_characterTasks.ContainsKey(character))
        {
            _characterTasks.Add(character, new List<ITask>());
        }
    }

    public ITask GetNextTask(Character character)
    {
        if (_characterTasks.TryGetValue(character, out var tasks))
        {
            ITask nextTask = tasks.OrderByDescending(task => task.Priority).FirstOrDefault();
            if (nextTask != null)
            {
                tasks.Remove(nextTask);
                return nextTask;
            }
            else
            {
                List<ITask> newTasks = GetNextTasksDependingOnTime(character);
                if (newTasks.Count > 0)
                {
                    tasks.AddRange(newTasks.Skip(1)); 
                    return newTasks[0]; 
                }
            }
        }
        return null;
    }

    public List<ITask> GetNextTasksDependingOnTime(Character character)
    {
        float currentTime = TimeManager.Instance.GetCurrentTimeinHours();
        List<ITask> newTasks = new List<ITask>();
        //newTasks.Add(new GoHomeTask(character));
        //newTasks.Add(new WaitTask(0.5f));

        if (currentTime >= 8.0f && currentTime <= 10.0f)
        {
            Debug.Log("work");
            newTasks.Add(new GoToWorkTask(character));
            newTasks.Add(new WaitTask(Random.Range(character.minWorkDuration, character.maxWorkDuration)));
        }

        else if (currentTime >= 12.0f && currentTime <= 13.0f)
        {
            Debug.Log("eat");
            newTasks.Add(new GoEatTask(character));
            newTasks.Add(new WaitTask(character.eatDuration));
        }

        else if (currentTime >= character.sleepTime - character.sleepTimeTolerance && currentTime < character.sleepTime + character.sleepTimeTolerance)
        {
            Debug.Log("sleep");
            newTasks.Add(new GoHomeTask(character));
            newTasks.Add(new WaitTask(Random.Range(character.minSleepDuration, character.maxSleepDuration)));
        }

        ////hobby

        else
        {
            newTasks.Add(new GoHomeTask(character));
            newTasks.Add(new WaitTask(1f));
        }
        return newTasks;
    }


}
