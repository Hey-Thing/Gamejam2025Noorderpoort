using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BeerBottle : MonoBehaviour
{
    public GameObject Cap;
    public Transform playerHead;
    public AudioClip drinkingSound;
    public float drinkDistance = 0.2f;
    public float drinkTiltAngle = 60f;
    public Material beerMaterial;
    public UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable capInteractable;
    public Rigidbody capRb;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private AudioSource audioSource;
    private bool isDrinking = false;
    public bool capRemoved = false;
    public bool isEmpty = false;
    private int drinkCount = 0;
    private float maxFill = 0.7f;
    private float fillStep;
    public drunkeffect drunkEffect;

    void Start()
    {
        grabInteractable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        audioSource = gameObject.AddComponent<AudioSource>();
        if (drinkingSound) audioSource.clip = drinkingSound;

        fillStep = 0.11f;
        beerMaterial.SetFloat("_Fill", maxFill);

        if (capInteractable != null)
        {
            capInteractable.enabled = false; // Cap can't be grabbed until beer is held
            capInteractable.selectEntered.AddListener(OnCapGrabbed);
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
        if (capRemoved) return; // Prevent multiple triggers

        Debug.Log("Cap removed!");
        Destroy(Cap);
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
        if (!grabInteractable.isSelected && isEmpty)

        {
            Destroy(gameObject);
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

                float newFill = maxFill - (drinkCount * fillStep);
                beerMaterial.SetFloat("_Fill", newFill);

                Debug.Log("Drinking beer! Fill level: " + newFill);

                if (drinkingSound) audioSource.Play();
            }
            else if (drinkCount == 4)
            {
                Debug.Log("Beer empty!");
               // isEmpty = true;
            }
        }
        else
        {
            isDrinking = false;
        }
    }


}