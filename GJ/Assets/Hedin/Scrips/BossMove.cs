using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
	public Transform[] waypoints;  // Array to store the waypoints
	public float speed = 3f;       // Speed of the AI movement
	public float reachThreshold = 1f; // Threshold to consider a waypoint reached
	public float waitTimeAtPoint2 = 2f; // Time to wait at waypoint 2 (in seconds)

	private int currentWaypointIndex = 0; // The current waypoint the AI is heading towards
	private bool isMovingForward = true; // A flag to check if the AI is moving forward or backward
	private bool isWaiting = false; // A flag to check if the AI is currently waiting at a waypoint

	void Start()
	{
		transform.position = waypoints[0].position;
	}

	void Update()
	{
		if (waypoints.Length > 0 && !isWaiting)
		{
			MoveToNextWaypoint();
		}
	}

	void MoveToNextWaypoint()
	{
		if (waypoints.Length == 0) return;

		Transform targetWaypoint = waypoints[currentWaypointIndex];

		Vector3 direction = targetWaypoint.position - transform.position;
		if (direction != Vector3.zero)
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime * 100); // Adjust rotation speed
		}

		
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

		
		if (Vector3.Distance(transform.position, targetWaypoint.position) < reachThreshold)
		{
			
			if (currentWaypointIndex == 2 && !isWaiting)
			{
				StartCoroutine(WaitAtPoint2());
			}
			else
			{
				
				if (isMovingForward)
				{
					
					currentWaypointIndex = (currentWaypointIndex + 1);
					
					if (currentWaypointIndex == waypoints.Length)
					{
						isMovingForward = false;
						currentWaypointIndex = waypoints.Length - 2;
					}
				}
				else
				{
					
					currentWaypointIndex = (currentWaypointIndex - 1);
					
					if (currentWaypointIndex == -1)
					{
						isMovingForward = true;
						currentWaypointIndex = 1;
					}
				}
			}
		}
	}

	private IEnumerator WaitAtPoint2()
	{
		isWaiting = true; 
		yield return new WaitForSeconds(waitTimeAtPoint2);
		isWaiting = false; 
		
		isMovingForward = false;
		currentWaypointIndex = waypoints.Length - 2; 
	}


}
