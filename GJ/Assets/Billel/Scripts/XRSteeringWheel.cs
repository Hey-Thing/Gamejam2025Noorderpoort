using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using System.Collections.Generic;

public class XRSteeringWheel : UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable
{
    [SerializeField] private Transform wheelCenter;

    private List<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor> interactors = new List<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor>();
    private Dictionary<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor, Vector3> lastHandDirections = new Dictionary<UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor, Vector3>();

    private float wheelRotation = 0f;

    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {
        base.OnSelectEntered(args);

        UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor;
        if (interactor != null)
        {
            interactors.Add(interactor);
            lastHandDirections[interactor] = GetHandDirection(interactor.transform.position);
        }
    }

    protected override void OnSelectExited(SelectExitEventArgs args)
    {
        base.OnSelectExited(args);

        UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor interactor = args.interactorObject as UnityEngine.XR.Interaction.Toolkit.Interactors.XRBaseInteractor;
        if (interactor != null)
        {
            interactors.Remove(interactor);
            lastHandDirections.Remove(interactor);
        }
    }

    private void Update()
    {
        if (interactors.Count > 0)
        {
            float totalAngleDelta = 0f;

            foreach (var interactor in interactors)
            {
                Vector3 currentHandDirection = GetHandDirection(interactor.transform.position);
                float angleDelta = Vector3.SignedAngle(lastHandDirections[interactor], currentHandDirection, transform.forward);

                totalAngleDelta += angleDelta;
                lastHandDirections[interactor] = currentHandDirection;
            }

            wheelRotation -= totalAngleDelta / interactors.Count; // Average rotation if two hands
            transform.localEulerAngles = new Vector3(0, 0, wheelRotation); // Rotate only around Z
        }
    }

    private Vector3 GetHandDirection(Vector3 handPosition)
    {
        Vector3 direction = handPosition - wheelCenter.position;
        direction.z = 0; // Ignore Z-axis movement
        return direction.normalized;
    }
}
