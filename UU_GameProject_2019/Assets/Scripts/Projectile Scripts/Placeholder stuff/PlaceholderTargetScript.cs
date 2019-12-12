using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderTargetScript : MonoBehaviour, IExplodable, IShootable
{
    public int health;

    void Start()
    {
        health = 3;
    }

    public void getExploded()
    {
        getDamaged();
    }

    public void getShot()
    {
        getDamaged();
    }

    public void getDamaged() //placeholder for Wietse's health system
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
