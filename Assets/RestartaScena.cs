using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class RestartaScena : MonoBehaviour
{
    
    public InputActionProperty AButton;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (AButton.action.ReadValue<float>() > 0.1f)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        }

    }
}
