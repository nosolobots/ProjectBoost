using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class QuitApplication : MonoBehaviour
{
    /*
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Quitting application");
            Application.Quit();
        }           
    }
    */
    public void Quit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Quitting application");
            Application.Quit();
        }
    }
}
