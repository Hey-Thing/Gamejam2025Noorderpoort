using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

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

        if (rightDir.y > 0.1f) 
        {
            transform.position += transform.right * speed * Time.deltaTime;
        }
        else if (rightDir.y < -0.1f) 
        {
            transform.position += -transform.right * speed * Time.deltaTime;
        }
    }
}
