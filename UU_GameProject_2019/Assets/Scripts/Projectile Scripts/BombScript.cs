using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class BombScript : MonoBehaviour, IExplodable, IBurnable
{
    [SerializeField] float fuseLength;
    [SerializeField] float blastRadius;
    public GameObject Explosion;

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

    public void Explode()
    {
        if(!hasExpoded)
        {
            GameObject newExplosion = Instantiate(Explosion, transform.position, transform.rotation) as GameObject;
            newExplosion.transform.parent = transform;
            Destroy(newExplosion, 1.0f);

            hasExpoded = true;
            Collider[] bombTargets = Physics.OverlapSphere(this.transform.position, blastRadius);
            for (int i = 0; i < bombTargets.Length; i++)
            {
                IExplodable explodable = bombTargets[i].gameObject.GetComponent<IExplodable>(); //IExplodable is the interface that tracks if something can interact with bombs in a special way

                if (explodable != null)
                    explodable.getExploded();
            }

            GetComponent<Renderer>().enabled = false;
            Destroy(gameObject, 1.0f);
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
}
