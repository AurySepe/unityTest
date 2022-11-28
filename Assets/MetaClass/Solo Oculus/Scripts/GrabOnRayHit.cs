using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;

public class GrabOnRayHit : MonoBehaviour
{
    [SerializeField]
    private GrabInteractable _grabInteractable;

    [SerializeField]
    private GrabInteractor _grabInteractor;
    // Start is called before the first frame update
    

    public void ForceGrab()
    {
        _grabInteractable.transform.position = _grabInteractor.transform.position;
        _grabInteractor.Select();
    }
}
