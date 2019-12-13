using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondCamera;
    public static string activeCamera;

    // Setting initial variables.
    void Start()
    {
        activeCamera = "mainCamera";
    }

    // Switches the camera depending on which one is set active.
    private void Update()
    {
        if(activeCamera == "mainCamera")
        {
            mainCamera.enabled = true;
            secondCamera.enabled = false;
        }
        if (activeCamera == "secondCamera")
        {
            mainCamera.enabled = false;
            secondCamera.enabled = true;
        }
    }
}
