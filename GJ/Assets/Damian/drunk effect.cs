using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class drunkeffect : MonoBehaviour
{ 
    public Volume volume;
    private LensDistortion lensDistortion;
    private MotionBlur motionBlur;

    public float shakeAmount = 0.05f;
    public float wobbleSpeed = 1.5f;  
    public float wobbleAmount = 5f;  

    private Vector3 originalPos;
    private float timer = 0f;

    void Start()
    {
        originalPos = transform.localPosition;

        if (volume.profile.TryGet(out lensDistortion) && volume.profile.TryGet(out motionBlur))
        {
            lensDistortion.intensity.Override(-0.1f); 
            motionBlur.intensity.Override(0.5f);     
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (lensDistortion != null)
        {
            lensDistortion.intensity.value = Mathf.Sin(timer * 2f) * 1f - 0.5f;
        }

        transform.localPosition = originalPos + (Vector3)Random.insideUnitCircle * shakeAmount;

        float tilt = Mathf.Sin(Time.time * wobbleSpeed) * wobbleAmount;
        transform.rotation = Quaternion.Euler(0, 0, tilt);
    }
}

