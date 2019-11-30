using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    public Camera mainCamera;
    public Camera secondCamera;
    public static string activeCamera;

    // Start is called before the first frame update
    void Start()
    {
        activeCamera = "mainCamera";
    }

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
