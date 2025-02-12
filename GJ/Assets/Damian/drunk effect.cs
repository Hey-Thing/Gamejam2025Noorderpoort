using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

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

        if (volume.profile.TryGet(out lensDistortion) && volume.profile.TryGet(out motionBlur))
        {
            lensDistortion.intensity.Override(beer); 
            motionBlur.intensity.Override(beer/2);     
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

        float tilt = Mathf.Sin(Time.time * beer) * beer;
        transform.rotation = Quaternion.Euler(0, 0, tilt);
    }
}

