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

    public IList<Character> GetCharacters()
    {
        return _characterTasks.Keys.ToList();
    }

    public void FinishCurrentTaskForAllCharacters()
    {
        foreach(Character character in _characterTasks.Keys)
        {
            character.FinishCurrentTask();
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
                //Debug.Log("Next task from list: " + nextTask.Name);
                tasks.Remove(nextTask);
                return nextTask;
            }
            else
            {
                List<ITask> newTasks = GetNextTasksDependingOnTime(character);
                if (newTasks.Count > 0)
                {
                    tasks.AddRange(newTasks.Skip(1)); 
                    //Debug.Log("Next task from time: " + newTasks[0].Name);
                    return newTasks[0]; 
                }
            }
        }
        return null;
    }

    public List<ITask> GetNextTasksDependingOnTime(Character character)
    {
        float currentTime = TimeManager.Instance.GetCurrentDayHours();
        List<ITask> newTasks = new List<ITask>();


        if (currentTime >= character.sleepTime - character.sleepTimeTolerance && currentTime < character.sleepTime + character.sleepTimeTolerance)
        {
            newTasks.Add(new GoHomeTask(character));
            newTasks.Add(new WaitAtCurrentDestinationTask(character, Random.Range(character.minSleepDuration, character.maxSleepDuration)));
        }
        else if (currentTime >= 12.0f && currentTime <= 14.0f)
        {
            newTasks.Add(new GoEatTask(character));
            newTasks.Add(new WaitAtCurrentDestinationTask(character, character.eatDuration));
        }
        else if (!character.wentToHobbyToday && character.wentToWorkToday)
        {
            newTasks.Add(new GoToHobbyTask(character));
            newTasks.Add(new WaitAtCurrentDestinationTask(character, character.hobbyDuration));
            character.wentToHobbyToday = true;
        }
        else if (character.remainingWorkHoursToday > 0)
        {
            newTasks.Add(new GoToWorkTask(character));
            newTasks.Add(new WaitAtCurrentDestinationTask(character, character.remainingWorkHoursToday / 2));
            character.remainingWorkHoursToday -= character.remainingWorkHoursToday / 2;
            character.wentToWorkToday = true;
        }
        else
        {
            newTasks.Add(new ExploreTask(character.GetNavMeshAgent(), PlaceManager.Instance.GetHomeForCharacter(character).transform.position, 50f, 1f));
        }
        return newTasks;
    }


}
