using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMovement : MonoBehaviour
{
    public float speed = 1f;
    public float RotatingSpeed = 1024;
    public GameObject Player;
    bool returning = false;
    
    Vector3 Destination, playerRelativePos;

    void Start()
    {
        Destination = BoomerangThrowing.Destination;
    }

    
    void Update()
    {
        Vector3 playerPos = GameObject.Find("ThirdPersonController").transform.position;
        playerRelativePos = new Vector3(playerPos.x, transform.position.y, playerPos.z);

        transform.Rotate(0, 0, RotatingSpeed * Time.deltaTime, Space.Self); //rotates 'RotatingSpeed' degrees per second around y axis
        
        if (Vector3.Distance(transform.position, Destination) < 5)
        {
            returning = true;
        }
        if (returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerRelativePos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, speed * Time.deltaTime);
        }
    }
    /*
    void Update()
    {
        Vector3 playerPos = GameObject.Find("ThirdPersonController").transform.position;
        transform.position = Vector3.MoveTowards(playerStartPos, new Vector3(10 * Mathf.Sin(playerRot.y), transform.position.y, 10 * Mathf.Cos(playerRot.y)), speed * Time.deltaTime);
    }*/
}