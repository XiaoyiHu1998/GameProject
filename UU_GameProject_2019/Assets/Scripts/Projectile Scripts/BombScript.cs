using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour, IExplodable, IBurnable
{
    public float fuseLength;
    public float blastRadius;

    float remainingFuse;
    bool hasExpoded; //this prevents an infinite loop where two bombs will cause eachother to explode repeatedly

    void Start()
    {
        remainingFuse = fuseLength;
        hasExpoded = false;
    }
    
    void Update()
    {
        remainingFuse -= Time.deltaTime; //slightly different implementation from arrows and beams since bombs need to call their own Explode() function instead of the basic Destroy() one
        if (remainingFuse <= 0)
        {
            Explode();
        }
    }

    public void getBurned() //bombs will cook off if they enter fire, this method comes from IBurnable
    {
        Explode();
    }

    public void getExploded() //bombs implement IExplodable to do sympathetic explosions
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
                IExplodable explodable = bombTargets[i].gameObject.GetComponent<IExplodable>(); //IExplodable is the interface that tracks if something can interact with bombs in a special way

                if (explodable != null)
                    explodable.getExploded();
            }

            Destroy(gameObject);
        }
    }
}
