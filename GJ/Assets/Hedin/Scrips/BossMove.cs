using System.Collections;
using UnityEngine;


public class BossMove : MonoBehaviour
{
	public Transform[] waypoints;  // Array to store the waypoints
	public float speed = 3f;       // Speed of the AI movement
	public float reachThreshold = 1f; // Threshold to consider a waypoint reached
	public float detectionRadius = 5f; // Radius around the player to trigger the stop
	public Transform player; // Reference to the player's transform
	public Camera mainCamera; // Reference to the main camera (for rotation towards it)
	public string beerBottleTag = "BeerBottle"; // Tag for the beer bottle object
	public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable beerBottleInteractable; // Reference to the XRGrabInteractable component for beer bottle

	private int currentWaypointIndex = 0; // The current waypoint the AI is heading towards
	private bool isMovingForward = true; // A flag to check if the AI is moving forward or backward
	private bool isWaiting = false; // A flag to check if the AI is currently waiting at a waypoint

	void Start()
	{
		// Make sure the AI starts at the first waypoint
		transform.position = waypoints[0].position;
	}

	void Update()
	{
		// If the player is within the danger radius and holding the beer bottle, stop moving and look at the camera
		if (IsPlayerInDangerArea() && IsPlayerHoldingBeerBottle())
		{
			StopAndLookAtCamera();
		}
		else
		{
			// Otherwise, move normally through the waypoints
			if (waypoints.Length > 0 && !isWaiting)
			{
				MoveToNextWaypoint();
			}
		}
	}

	// Check if the player is within the danger radius
	bool IsPlayerInDangerArea()
	{
		if (player != null)
		{
			float distance = Vector3.Distance(player.position, transform.position);
			return distance <= detectionRadius;
		}
		return false;
	}

	// Check if the player is holding the beer bottle
	bool IsPlayerHoldingBeerBottle()
	{
		return beerBottleInteractable != null && beerBottleInteractable.isSelected;
	}

	// Stop moving and rotate to look at the camera
	void StopAndLookAtCamera()
	{
		// Stop moving
		speed = 0f;

		// Rotate towards the main camera (without moving)
		Vector3 direction = mainCamera.transform.position - transform.position;
		direction.y = 0; // Keep the rotation only on the Y-axis to avoid tilting
		Quaternion targetRotation = Quaternion.LookRotation(direction);
		transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime * 100);
	}

	// Move to the next waypoint (normal movement behavior)
	void MoveToNextWaypoint()
	{
		if (waypoints.Length == 0) return;

		// Determine the target waypoint based on direction
		Transform targetWaypoint = waypoints[currentWaypointIndex];

		// Rotate the AI to look at the target waypoint
		Vector3 direction = targetWaypoint.position - transform.position;
		if (direction != Vector3.zero) // Avoid any division by zero
		{
			Quaternion targetRotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime * 100); // Adjust rotation speed
		}

		// Move towards the waypoint
		float step = speed * Time.deltaTime;
		transform.position = Vector3.MoveTowards(transform.position, targetWaypoint.position, step);

		// Check if the AI has reached the waypoint
		if (Vector3.Distance(transform.position, targetWaypoint.position) < reachThreshold)
		{
			// If moving forward and we reach the last waypoint, switch direction
			if (isMovingForward)
			{
				// Move to the next waypoint forward
				currentWaypointIndex = (currentWaypointIndex + 1);
				// If we reach the last waypoint, change direction
				if (currentWaypointIndex == waypoints.Length)
				{
					isMovingForward = false; // Start moving backward
					currentWaypointIndex = waypoints.Length - 2; // Set to the second last waypoint
				}
			}
			else
			{
				// Move to the previous waypoint backward
				currentWaypointIndex = (currentWaypointIndex - 1);
				// If we reach the first waypoint, change direction
				if (currentWaypointIndex == -1)
				{
					isMovingForward = true; // Start moving forward again
					currentWaypointIndex = 1; // Set to the second waypoint (index 1)
				}
			}
		}
	}
}
