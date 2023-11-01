using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager Instance { get; private set; }

    public float festivalTriggerTime = 24f;
    public float festivalDuration = 3f;

    private float _lastFestivalTime;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _lastFestivalTime = TimeManager.Instance.GetTimeSinceStartInHours()-23f;
    }

    private void Update()
    {
        if (TimeManager.Instance.GetTimeSinceStartInHours() - _lastFestivalTime >= festivalTriggerTime)
        {
            TriggerFestival();
            _lastFestivalTime = TimeManager.Instance.GetTimeSinceStartInHours();
        }
    }

    private void TriggerFestival()
    {
        Debug.Log("Festival triggered!");
        foreach (Character character in TaskManager.Instance.GetCharacters())
        {
            TaskManager.Instance.AddTask(character, new GoToFestivalTask(character));
            TaskManager.Instance.AddTask(character, new CelebrateTask(character, 7f, festivalDuration));
        }
        TaskManager.Instance.FinishCurrentTaskForAllCharacters();
    }
}


