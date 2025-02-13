using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.InputSystem;


public class drunkeffect : MonoBehaviour
{ 
    public Volume volume;
    private LensDistortion lensDistortion;
    private MotionBlur motionBlur;
    private Vignette vignette;

    public float wobbleSpeed;
    public float wobbleAmount;  
    
    public float vignetteIntesetie=1;
    public float vignettesmooth=1;


    private Vector3 originalPos;
    private float timer = 0f;

    public float lensD = 1f;
    public float beer = 0f;

    void Start()
    {
        originalPos = transform.localPosition;

        if (volume.profile.TryGet(out lensDistortion) &&
             volume.profile.TryGet(out motionBlur) &&
             volume.profile.TryGet(out vignette)) 
        {
            Debug.Log("Vignette effect found and assigned.");
        }
        else
        {
            Debug.LogError("One or more effects not found in the Volume profile!");
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Sin(timer * 2f) * beer/lensD - beer/lensD/2;
        }

        if (vignette != null)
        {
            vignette.intensity.value = beer * vignetteIntesetie;
            vignette.smoothness.value = beer * vignettesmooth;
        }

        float tilt = Mathf.Sin(Time.time * beer * wobbleSpeed) * beer * wobbleAmount;
        transform.rotation = Quaternion.Euler(0, 0, tilt);

    }
}

