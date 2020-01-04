using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float deltaZ;

    void Start()
    {
        deltaZ = 5;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 playerPosition = GameObject.Find("ThirdPersonController").transform.position;
        Camera.main.transform.position = new Vector3(playerPosition.x, playerPosition.y + 5, playerPosition.z - deltaZ);
    }
}
