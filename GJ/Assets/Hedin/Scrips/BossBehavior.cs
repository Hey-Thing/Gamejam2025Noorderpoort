using UnityEngine;


public class BossBehavior : MonoBehaviour
{
	public Transform player;            // Reference to the player�s position
	public Camera mainCamera;           // Reference to the main camera
	public float detectionRadius = 5f;  // Radius for detecting the player
	public string beerBottleTag = "BeerBottle"; // Tag for the beer bottle object
	public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable beerBottleInteractable; // Reference to the beer bottle's XRInteractable component

	private bool isLookingAtCamera = false;

	void Update()
	{
		// Check if the player is holding the beer bottle and within the danger radius
		if (IsPlayerInDangerArea() && IsPlayerHoldingBeerBottle())
		{
			// Stop and rotate the boss to look at the camera
			StopAndLookAtCamera();
		}
		else
		{
			// Resume movement if the conditions are not met
			ResumeMovement();
		}
	}

	// Check if the player is within the danger area
	bool IsPlayerInDangerArea()
	{
		if (player != null)
		{
			float distance = Vector3.Distance(player.position, transform.position);
			return distance <= detectionRadius; // Within detection radius
		}
		return false;
	}

	// Check if the player is holding the beer bottle
	bool IsPlayerHoldingBeerBottle()
	{
		return beerBottleInteractable != null && beerBottleInteractable.isSelected; // The beer bottle is selected
	}

	// Stop the boss's movement and make it look at the camera
	void StopAndLookAtCamera()
	{
		if (!isLookingAtCamera)
		{
			// Stop moving (here you can set speed to 0, or handle stopping if needed)
			// Example: Set a boolean flag or stop any movement logic
			isLookingAtCamera = true;

			// Look at the camera (only rotating on the Y-axis)
			Vector3 direction = mainCamera.transform.position - transform.position;
			direction.y = 0; // Ignore vertical movement
			Quaternion rotation = Quaternion.LookRotation(direction);
			transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 100f * Time.deltaTime); // Smooth rotation
		}
	}

	// If conditions are not met, resume normal movement (placeholder for any movement logic you might have)
	void ResumeMovement()
	{
		if (isLookingAtCamera)
		{
			// Resume normal movement (or reset the isLookingAtCamera flag)
			isLookingAtCamera = false;
			// Add any logic to resume movement, such as starting movement scripts, etc.
		}
	}
}
