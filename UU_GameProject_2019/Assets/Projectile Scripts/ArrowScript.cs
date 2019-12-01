using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    void Start()
    {
        
    }
    
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision target)
    {
        IShootable shootable = target.gameObject.GetComponent<IShootable>();

        if (shootable != null)
        {
            shootable.getShot();
        }

        Destroy(gameObject);
    }
}
