using System.Collections;
using System.Collections.Generic;
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


    public XRRayInteractor leftRay;

    public XRRayInteractor rightRay;
    // Start is called before the first frame update
    void Start()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        bool isLeftRayHovering = leftRay.TryGetHitInfo(out Vector3 leftPos,out Vector3 leftNormal,out int leftNumber,out bool leftValid);
        
        LeftRay.SetActive(!isLeftRayHovering && leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
        
        bool isRightRayHovering = rightRay.TryGetHitInfo(out Vector3 rightPos,out Vector3 rightNormal,out int rightNumber,out bool rightValid);

        
        RightRay.SetActive( !isRightRayHovering && rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);
    }
}