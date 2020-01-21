using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public PlayerInventory Owner;

    void OnTriggerEnter(Collider target)
    {
        IShootable shootable = target.gameObject.GetComponent<IShootable>(); //Ishootable is the interface that tracks if something can interact with arrows in a special way

        if (shootable != null)
        {
            shootable.getShot();
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

        MoneyDropScript moneyDrop = target.gameObject.GetComponent<MoneyDropScript>();

        if (moneyDrop != null)
        {
            Owner.lootMoney(moneyDrop.amount);
            Destroy(moneyDrop.gameObject);
        }

        Destroy(gameObject);
    }

    public void SetOwner(PlayerInventory owner)
    {
        Owner = owner;
    }
}
