using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class steeringwheel : MonoBehaviour
{
    public Transform steer;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = steer.position;


    }

}
