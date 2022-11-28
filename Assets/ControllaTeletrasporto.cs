using System.Collections;
using System.Collections.Generic;
using Oculus.Interaction;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class ControllaTeletrasporto : MonoBehaviour
{

    public GameObject LeftRay;

    public GameObject RightRay;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;
    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;
    public RayInteractor rayInteractorLeft;
    public RayInteractor rayInteractorRight;
    
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {


        LeftRay.SetActive(!rayInteractorLeft.HasCandidate && leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
        

        
        RightRay.SetActive(!rayInteractorRight.HasCandidate && rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}
