using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ControllaTeletrasporto : MonoBehaviour
{

    public GameObject LeftRay;

    public GameObject RightRay;

    public InputActionProperty leftActivate;
    public InputActionProperty rightActivate;
    public InputActionProperty leftCancel;
    public InputActionProperty rightCancel;

    // Start is called before the first frame update
    void Start()
    {
        LeftRay.SetActive(leftCancel.action.ReadValue<float>() == 0 && leftActivate.action.ReadValue<float>() > 0.1f);
        RightRay.SetActive(rightCancel.action.ReadValue<float>() == 0 && rightActivate.action.ReadValue<float>() > 0.1f);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
