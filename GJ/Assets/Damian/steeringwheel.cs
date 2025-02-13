using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class steeringwheel : MonoBehaviour
{
    public Transform steer;
    public float rotationSpeed = 100f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = steer.position;

        float rotation = 0f;

        if (Input.GetKey(KeyCode.D))
        {
            rotation = -rotationSpeed * Time.deltaTime;
        }
        else if (Input.GetKey(KeyCode.A))
        {
            rotation = rotationSpeed * Time.deltaTime;
        }

        transform.Rotate(0f, rotation, 0f);

    }

}
