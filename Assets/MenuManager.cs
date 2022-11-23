using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MenuManager : MonoBehaviour
{

    public GameObject menu;

    public InputActionProperty showButton;

    public Transform head;

    public float menuDistance = 2;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (showButton.action.WasPerformedThisFrame())
        {
            menu.SetActive(!menu.activeSelf);

            menu.transform.position = head.position + head.forward.normalized  * menuDistance;
        }
        menu.transform.LookAt(head.position);
        menu.transform.forward *= -1;


    }
}
