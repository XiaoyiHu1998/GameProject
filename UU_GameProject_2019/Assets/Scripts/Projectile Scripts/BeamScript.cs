using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour
{
    public float speed = 10f;
    
    public ProjectileLaunchScript Owner;

    void Start()
    {
        transform.Rotate(0, 90, 0, Space.Self); //beam is wider than it is long, so it is rotated
    }
    
    void Update()
    {
        transform.position += transform.forward * Time.deltaTime * speed;
    }

    void OnTriggerEnter(Collider target)
    {
        print("hit something");

        IStabable stabable = target.gameObject.GetComponent<IStabable>(); //IStabable is the interface that tracks if something can interact with the sword or it's beam in a special way

        if (stabable != null)
        {
            stabable.getStabbed();
        }

        ItemDropScript lootDrop = target.gameObject.GetComponent<ItemDropScript>();

        if (lootDrop != null)
        {
            Owner.lootObject(lootDrop.weapon, lootDrop.amount);
            Destroy(lootDrop.gameObject);
        }

        HealthDropScript healthDrop = target.gameObject.GetComponent<HealthDropScript>();

        if (healthDrop != null)
        {
            Owner.lootHealth(healthDrop.amount);
            Destroy(healthDrop.gameObject);
        }
    }

    public void SetOwner(ProjectileLaunchScript owner)
    {
        Owner = owner;
    }

    public void SetTimer(float timer)
    {
        Destroy(gameObject, timer);
    }
}