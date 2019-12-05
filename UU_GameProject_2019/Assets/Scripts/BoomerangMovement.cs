using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangMovement : MonoBehaviour
{
    public float delay = 0.2f;
    Vector3 velocity;
    float maxVelocity;
    public float returnRate = 0.5f;
    public GameObject Player;
    bool returning = false;

    void Start()
    {
        Invoke("wait", delay);
    }

    void wait()
    {
        returning = true;
    }

    void Update()
    {
        if (returning)
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            rb.velocity = rb.velocity + (Player.transform.position - transform.position).normalized * returnRate;
        }
    }

    /*
    void Update()
    {
        transform.position += vel; 
        if (delay > 0) 
            delay -= Time.deltaTime;
        else
        {
            vel = vel + (followThis.transfom.position - transform.position).normalized * Time.deltaTime * returnRate);
            vel = ve.normalized * Mathf.Clamp(vel.magnitude, 0, maxVel);
        }
    }*/
}