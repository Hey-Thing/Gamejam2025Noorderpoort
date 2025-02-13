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
        if (waypoints.Length > 0 && !isWaiting)
        {
            StartCoroutine(MoveToNextWaypoint()); // Start the coroutine properly
        }
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
    IEnumerator MoveToNextWaypoint()
    {
        if (waypoints.Length == 0) yield break;
        if (isWaiting) yield break; // Prevent multiple coroutines running

        isWaiting = true; // Set waiting flag

        // Determine the target waypoint based on direction
        Transform targetWaypoint = waypoints[currentWaypointIndex];

        // Rotate the AI to look at the target waypoint
        Vector3 direction = targetWaypoint.position - transform.position;
        if (direction != Vector3.zero) // Avoid division by zero
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
            // Stop moving and wait for 3 seconds
            float originalSpeed = speed;
            speed = 0f;
            yield return new WaitForSeconds(3f); // Wait time
            speed = originalSpeed; // Resume movement

            // Handle waypoint switching logic
            if (isMovingForward)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex == waypoints.Length)
                {
                    isMovingForward = false;
                    currentWaypointIndex = waypoints.Length - 2;
                }
            }
            else
            {
                currentWaypointIndex--;
                if (currentWaypointIndex == -1)
                {
                    isMovingForward = true;
                    currentWaypointIndex = 1;
                }
            }
        }

        isWaiting = false; // Reset waiting flag
    }

}
