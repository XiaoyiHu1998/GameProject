using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowThisUpScript : MonoBehaviour, IExplodable
{
    public void getExploded()
    {
        Destroy(gameObject);
    }

    void OnTriggerEnter(Collider target)
    {
        BombScript bomb = target.gameObject.GetComponent<BombScript>();

        if (bomb != null)
        {
            bomb.Explode();
        }
    }
}