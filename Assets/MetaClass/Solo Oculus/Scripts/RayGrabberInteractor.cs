using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using Oculus.Interaction.Surfaces;
using UnityEngine;

public class RayGrabberInteractor : RayInteractor
{
    protected override void InteractableSelected(RayInteractable interactable)
    {
        GrabInteractable grabInteractable = interactable.GetComponent<GrabInteractable>();
        if (grabInteractable != null)
        {
            interactable.transform.position = _grabInteractor.transform.position;
            group.Unselect();
            group.Unhover();
            group.Preprocess();
            group.Process();
            group.ProcessCandidate();
            group.Hover();
            group.Select();

        }
        ;
    }

    [SerializeField]
    private GrabInteractor _grabInteractor;

    [SerializeField]
    private InteractorGroup group;
}
