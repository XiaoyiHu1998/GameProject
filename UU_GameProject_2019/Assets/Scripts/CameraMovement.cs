using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // The delta distance between player and camera.
    public float deltaZ;

    void Start()
    {
        deltaZ = 5;
    }

    /// <summary>
    /// Follows the players position and makes adjustment in the z position.
    /// </summary>
    void LateUpdate()
    {
        Vector3 playerPosition = GameObject.Find("ThirdPersonController").transform.position;
        Camera.main.transform.position = new Vector3(playerPosition.x, transform.position.y, playerPosition.z - deltaZ);
    }
}
