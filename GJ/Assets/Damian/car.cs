using UnityEngine;

public class CarSteering : MonoBehaviour
{
    public Transform wheel;  // Reference to the wheel
    public float speed = 10f;

    void Update()
    {
        if (wheel == null)
        {
            Debug.LogWarning("Wheel reference is missing!");
            return;
        }

        Quaternion wheelRotation = wheel.rotation;

        Vector3 rightDir = wheelRotation * Vector3.right;

        if (rightDir.x > 0.1f) 
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if (rightDir.x < -0.1f) 
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }
}
