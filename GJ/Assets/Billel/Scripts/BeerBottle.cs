using UnityEngine;


public class BeerBottle : MonoBehaviour
{
    public Transform playerHead;
    public AudioClip drinkingSound;
    public float drinkDistance = 0.2f;
    public float drinkTiltAngle = 60f;
    public Material beerMaterial; // Assign the beer liquid material
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private AudioSource audioSource;

    private bool isDrinking = false;
    private int drinkCount = 0;
    private float maxFill = 0.16f;
    private float minFill = 0f;
    private float fillStep;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        audioSource = gameObject.AddComponent<AudioSource>();
        if (drinkingSound) audioSource.clip = drinkingSound;

        fillStep = maxFill / 4f; // 4 equal drinking steps
        beerMaterial.SetFloat("_Fill", maxFill);
    }

    void Update()
    {
        if (grabInteractable.isSelected)
        {
            CheckDrinking();
        }
        else
        {
            isDrinking = false;
        }
    }

    void CheckDrinking()
    {
        if (playerHead == null) return;

        float distance = Vector3.Distance(transform.position, playerHead.position);
        float tilt = Vector3.Angle(transform.up, Vector3.down);

        if (distance < drinkDistance && tilt > drinkTiltAngle)
        {
            if (!isDrinking && drinkCount < 4) // Ensure max 4 drinks
            {
                isDrinking = true;
                drinkCount++;
                float newFill = Mathf.Max(minFill, maxFill - (fillStep * drinkCount));
                beerMaterial.SetFloat("_Fill", newFill);

                Debug.Log("Drinking beer! Fill level: " + newFill);

                if (drinkingSound) audioSource.Play();
            }
        }
        else
        {
            isDrinking = false;
        }
    }
}
