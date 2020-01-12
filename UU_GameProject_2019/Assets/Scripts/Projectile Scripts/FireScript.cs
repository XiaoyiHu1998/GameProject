using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireScript : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider target)
    {
        IBurnable burnable = target.gameObject.GetComponent<IBurnable>(); //IBurnable is the interface that tracks if something can interact with fire in a special way

        if (burnable != null)
        {
            burnable.getBurned();
        }
    }
}