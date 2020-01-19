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
            //CharacterControl Script = GameObject.FindObjectOfType(typeof(CharacterControl)) as CharacterControl;
            //Script.TakeDamage();
            Destroy(gameObject);
        }
        else if (target.gameObject.tag == "Untagged")
        {
            Destroy(gameObject);
        }
    }
}