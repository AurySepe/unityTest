using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerMovementTutorial : MonoBehaviour
{

    public float sprintSpeed;
    


    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;


    private void Start()
    {

    }

    private void Update()
    {
        // ground check

        MyInput();
       
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }

    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
        
    }

    private void MovePlayer()
    {
        // calculate movement direction
        moveDirection = (orientation.forward * verticalInput + orientation.right * horizontalInput).normalized;
        Vector3 translation = moveDirection * sprintSpeed;
        Debug.Log(translation);
        transform.position += translation;

        
    }

   
    
    
}