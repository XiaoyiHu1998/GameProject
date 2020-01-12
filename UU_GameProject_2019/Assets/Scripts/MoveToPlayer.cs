using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{
    float speed, rotationSpeed;
    
    // Setting initial variables.
    void Start()
    {
        speed = 1;
        rotationSpeed = 3;
    }

    /// <summary>
    /// Fetches the player position, this must happen in the update statement since the player moves.
    /// If a certain distance between object and player is met it moves and rotates towards the player.
    /// </summary>
    void Update()
    {
        Vector3 playerPosition = GameObject.Find("ThirdPersonController").transform.position;
        if(Vector3.Distance(transform.position,playerPosition) < 4)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(playerPosition - transform.position, Vector3.up).eulerAngles), Time.deltaTime * rotationSpeed);
        }

    }
}
