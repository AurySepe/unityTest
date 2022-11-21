using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XrGrabInteractableTwoAttach : XRGrabInteractable
{

    public Transform leftHandAttach;
    
    public Transform rightHandAttach;


    protected override void OnSelectEntered(SelectEnterEventArgs args)
    {

        if (args.interactorObject.transform.CompareTag("Right hand"))
        {
            attachTransform = rightHandAttach;
        }
        else if(args.interactorObject.transform.CompareTag("Left hand"))
        {
            attachTransform = leftHandAttach;
        }
        
        base.OnSelectEntered(args);
    }
}
