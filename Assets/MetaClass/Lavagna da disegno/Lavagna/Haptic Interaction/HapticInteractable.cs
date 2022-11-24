using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

[System.Serializable]
public class Haptic
{
    [Range(0,1)]
    public float intensity;

    public float duration;

    public void TriggerHaptic(BaseInteractionEventArgs args)
    {
        if (args.interactorObject is XRBaseControllerInteractor controllerInteractor)
        {
            TriggerHaptic(controllerInteractor);
        }
    }

    private void TriggerHaptic(XRBaseControllerInteractor controllerInteractor)
    {
        if (intensity > 0)
        {
            controllerInteractor.SendHapticImpulse(intensity, duration);
        }
    }
}


public class HapticInteractable : MonoBehaviour
{
    public Haptic vibration;
    // Start is called before the first frame update
    void Start()
    {
        XRBaseInteractable interactable = GetComponent<XRBaseInteractable>();
        interactable.hoverEntered.AddListener(vibration.TriggerHaptic);
    }

    
}
