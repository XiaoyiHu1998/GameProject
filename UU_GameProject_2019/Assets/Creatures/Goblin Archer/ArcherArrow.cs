using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class ArcherArrow : MonoBehaviour
{
    void OnTriggerEnter(Collider target)
    {
        if (target.gameObject.tag == "Player")
        {
            HealthScript healthScript = GameObject.FindObjectOfType(typeof(HealthScript)) as HealthScript;
            healthScript.TakeDamage();
            Destroy(gameObject);
        }
        else if (target.gameObject.tag == "Untagged")
        {
            Destroy(gameObject);
        }
    }
}