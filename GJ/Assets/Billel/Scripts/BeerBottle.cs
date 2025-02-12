using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BeerBottle : MonoBehaviour
{
    public Transform playerHead;
    public AudioClip drinkingSound;
    public float drinkDistance = 0.2f;
    public float drinkTiltAngle = 60f;
    public Material beerMaterial;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable capInteractable; // Assign in Inspector
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private AudioSource audioSource;
    private FixedJoint capJoint; // Keeps cap attached

    private bool isDrinking = false;
    private bool capRemoved = false;
    private int drinkCount = 0;
    private float maxFill = 0.7f;
    private float minFill = 0f;
    private float fillStep;
    public drunkeffect drunkEffect;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        audioSource = gameObject.AddComponent<AudioSource>();
        if (drinkingSound) audioSource.clip = drinkingSound;

        fillStep = maxFill / 4f;
        beerMaterial.SetFloat("_Fill", maxFill);

        if (capInteractable != null)
        {
            capInteractable.enabled = false;
            capInteractable.selectEntered.AddListener(OnCapGrabbed);

            // Attach cap to bottle using FixedJoint
            capJoint = capInteractable.gameObject.AddComponent<FixedJoint>();
            capJoint.connectedBody = GetComponent<Rigidbody>();
        }

        grabInteractable.selectEntered.AddListener(OnBeerGrabbed);
        grabInteractable.selectExited.AddListener(OnBeerDropped);
    }

    void OnBeerGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Beer grabbed! Now the cap can be removed.");
        if (capInteractable != null)
            capInteractable.enabled = true;
    }

    void OnBeerDropped(SelectExitEventArgs args)
    {
        if (!capRemoved)
        {
            Debug.Log("Beer dropped! Cap can't be removed anymore.");
            if (capInteractable != null)
                capInteractable.enabled = false;
        }
    }

    void OnCapGrabbed(SelectEnterEventArgs args)
    {
        Debug.Log("Cap grabbed! Removing FixedJoint.");

        // Remove FixedJoint so the cap detaches
        if (capJoint != null)
        {
            Destroy(capJoint);
        }

        capRemoved = true;
    }

    void Update()
    {
        if (grabInteractable.isSelected && capRemoved)
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
            if (!isDrinking && drinkCount < 4)
            {
                isDrinking = true;
                drinkCount++;
                drunkEffect.beer++;
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
