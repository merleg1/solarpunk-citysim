using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public static TimeManager Instance { get; private set; }

    public float timeScale = 1.0f; // Adjust the speed of in-game time.
    public DateTime CurrentTime { get; private set; }

    private float _elapsedGameSeconds;

    public float GetCurrentTimeinHours()
    {
        return CurrentTime.Hour + CurrentTime.Minute / 60f;
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
        CurrentTime = DateTime.Now;
        _elapsedGameSeconds = 0f;
    }

    private void Update()
    {
        float realTimeSeconds = Time.deltaTime;
        _elapsedGameSeconds += realTimeSeconds * timeScale;

        if (_elapsedGameSeconds >= 60)
        {
            _elapsedGameSeconds = 0;
            CurrentTime = CurrentTime.AddMinutes(1);
        }
    }
}
