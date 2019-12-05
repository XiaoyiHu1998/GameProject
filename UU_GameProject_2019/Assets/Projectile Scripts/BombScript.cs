using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour, IExplodable, IBurnable
{
    public float fuseLength;
    public float blastRadius;

    float remainingFuse;
    bool hasExpoded;
    
    void Start()
    {
        remainingFuse = fuseLength;
        hasExpoded = false;
    }
    
    void Update()
    {
        remainingFuse -= Time.deltaTime;
        if (remainingFuse <= 0)
        {
            Explode();
        }
    }

    public void getBurned()
    {
        Explode();
    }

    public void getExploded()
    {
        Explode();
    }

    public void Explode()
    {
        if(!hasExpoded)
        {
            hasExpoded = true;
            Collider[] bombTargets = Physics.OverlapSphere(this.transform.position, blastRadius);
            for (int i = 0; i < bombTargets.Length; i++)
            {
                IExplodable explodable = bombTargets[i].gameObject.GetComponent<IExplodable>();

                if (explodable != null)
                    explodable.getExploded();
            }

            Destroy(gameObject);
        }
    }
}
