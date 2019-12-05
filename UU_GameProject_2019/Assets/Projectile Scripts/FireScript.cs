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

    void OnCollisionEnter(Collision target)
    {
        IBurnable burnable = target.gameObject.GetComponent<IBurnable>();

        if (burnable != null)
        {
            burnable.getBurned();
        }
    }
}
