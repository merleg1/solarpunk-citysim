using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drone : MonoBehaviour
{
    public float speed = 1f;
    public float targetTolerance = 0.1f;

    private Home currentTargetHome;
    private bool isMoving = false;

    private void Start()
    {
        speed *= TimeManager.Instance.timeScale;
        MoveToRandomHome();
    }

    private void Update()
    {
        if (!isMoving)
        {
            MoveToRandomHome();
        }
        else if (currentTargetHome != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, currentTargetHome.transform.position);
            if (distanceToTarget <= targetTolerance)
            {
                isMoving = false;
            }
        }
    }

    private void MoveToRandomHome()
    {
        Place randomPlace = PlaceManager.Instance.GetRandomPlace(Place.PlaceType.Home);

        if (randomPlace != null)
        {
            currentTargetHome = (Home)randomPlace;
            isMoving = true;

            StartCoroutine(MoveToTarget(currentTargetHome.transform.position));
        }
    }

    private IEnumerator MoveToTarget(Vector3 targetPosition)
    {
        while (Vector3.Distance(transform.position, targetPosition) > targetTolerance)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            yield return null;
        }
    }
}
