using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    void Start()
    {
        //if arrows glitching through world and existing perpetually becomes a problem, call a timed Destroy here
    }

    void Update() 
    {
        
    }

    void OnCollisionEnter(Collision target)
    {
        IShootable shootable = target.gameObject.GetComponent<IShootable>(); //Ishootable is the interface that tracks if something can interact with arrows in a special way

        if (shootable != null)
        {
            shootable.getShot();
        }

        Destroy(gameObject);
    }
}
