using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Car : MonoBehaviour
{
    public float speed = 5;
    private float basespeed = 5;
    public float strafespeed = 10;

    public XRNode inputSource = XRNode.RightHand;  
    private InputDevice device;
    public Transform steeringWheel;  
    private float rotationInput = 0f; 
    private float currentRotation = 0f; 

    void Start()
    {
        device = InputDevices.GetDeviceAtXRNode(inputSource);
    }

    void Update()
    {
        if (device.isValid)
        {
            Vector2 primary2DAxisValue;
            if (device.TryGetFeatureValue(CommonUsages.primary2DAxis, out primary2DAxisValue))
            {
                rotationInput = primary2DAxisValue.x;
            }

            RotateSteeringWheel(rotationInput);

            transform.position += transform.forward * speed * Time.deltaTime;
        }
    }

    void RotateSteeringWheel(float input)
    {
        float steeringAmount = input * 200f;  
        currentRotation += steeringAmount * Time.deltaTime;

        currentRotation = Mathf.Clamp(currentRotation, -90f, 90f);

        steeringWheel.localRotation = Quaternion.Euler(0, currentRotation, 0); 
    }

    public void AdjustSpeed(float multiplier)
    {
        speed = basespeed * multiplier;
    }
}
