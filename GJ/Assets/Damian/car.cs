using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class car : MonoBehaviour
{
    public float speed = 5;
    private float basespeed = 5;
    public float strafespeed = 10;
    void Start()
    {

    }

    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;

        float horizontal = Input.GetAxis("Horizontal");
        transform.position += transform.right * horizontal * strafespeed * Time.deltaTime;
    }

    public void AdjustSpeed(float multiplier)
    {
        speed = basespeed * multiplier;
    }
}
