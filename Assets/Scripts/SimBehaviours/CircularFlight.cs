using UnityEngine;

public class CircularFlight : MonoBehaviour
{
    public float radius = 10.0f; 
    public float rotationSpeed = 30.0f; 

    private Vector3 centerPoint;
    private float angle = 0.0f;

    private void Start()
    {
        centerPoint = transform.position;
        rotationSpeed *= TimeManager.Instance.timeScale;
    }

    private void Update()
    {
        float x = centerPoint.x + radius * Mathf.Cos(angle * Mathf.Deg2Rad);
        float z = centerPoint.z + radius * Mathf.Sin(angle * Mathf.Deg2Rad);

        float rotationAngle = Mathf.Atan2(z - transform.position.z, x - transform.position.x) * Mathf.Rad2Deg;

        transform.position = new Vector3(x, transform.position.y, z);

        transform.rotation = Quaternion.Euler(0, rotationAngle, 0);

        angle += rotationSpeed * Time.deltaTime;

        if (angle >= 360.0f)
        {
            angle = 0.0f;
        }
    }
}
