using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ActivateGrabRay : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject leftGrabRay;
    public GameObject rightGrabRay;
    
    public XRDirectInteractor rightHandInteractor;
    public XRDirectInteractor leftHandInteractor;


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        leftGrabRay.SetActive(leftHandInteractor.interactablesSelected.Count == 0);
        rightGrabRay.SetActive(rightHandInteractor.interactablesSelected.Count == 0);

    }
}
