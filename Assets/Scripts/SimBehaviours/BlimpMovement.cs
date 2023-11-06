using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlimpMovement : MonoBehaviour
{
    public float speed = 1f;
    public float rotationSpeed = 50f;
    public List<Transform> waypoints = new List<Transform>();

    private int currentWaypointIndex = 0;
    private bool isRotating = true;

    private void Start()
    {
        currentWaypointIndex = 0;
        isRotating = true;
    }

    private void Update()
    {
        if (waypoints.Count > 0)
        {
            Vector3 currentWaypointPosition = waypoints[currentWaypointIndex].position;
            Vector3 direction = currentWaypointPosition - transform.position;
            float distanceToWaypoint = direction.magnitude;

            if (distanceToWaypoint <= 1f)
            {
                if (currentWaypointIndex == waypoints.Count - 1)
                {
                    currentWaypointIndex = 0;
                }
                else
                {
                    currentWaypointIndex++;
                }
                isRotating = true;
            }
            else
            {
                if (isRotating)
                {
                    RotateTowardsNextWaypoint(currentWaypointPosition, direction);
                }
                else
                {
                    MoveTowardsWaypoint(direction);
                }
            }
        }
    }

    private void RotateTowardsNextWaypoint(Vector3 nextWaypointPosition, Vector3 direction)
    {
        Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * rotationSpeed);

        if (Quaternion.Angle(transform.rotation, rotation) < 1f)
        {
            isRotating = false;
        }
    }

    private void MoveTowardsWaypoint(Vector3 direction)
    {
        transform.Translate(direction.normalized * speed * Time.deltaTime, Space.World);
    }
}
