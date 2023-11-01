using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public float timeScale = 1.0f; // Adjust the speed of in-game time.
    public DateTime CurrentTime { get; private set; }

    private DateTime _startTime;

    private float _elapsedGameSeconds;

    public float GetCurrentDayHours()
    {
        return CurrentTime.Hour + CurrentTime.Minute / 60f;
    }

    public float GetTimeSinceStartInHours()
    {
        return (float)(CurrentTime - _startTime).TotalHours;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        _startTime = DateTime.Today.AddHours(9);

        CurrentTime = _startTime;
        _elapsedGameSeconds = 0f;
    }

    private void Update()
    {
        float realTimeSeconds = Time.deltaTime;
        _elapsedGameSeconds += realTimeSeconds * timeScale;

        while (_elapsedGameSeconds >= 60)
        {
            _elapsedGameSeconds -= 60;
            CurrentTime = CurrentTime.AddMinutes(1);
        }
    }
}
