using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderTargetScript : MonoBehaviour, IExplodable
{
    public int health;

    // Start is called before the first frame update
    void Start()
    {
        health = 3;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void getExploded()
    {
        getDamaged();
    }

    public void getDamaged() //placeholder tot health en zo klaar zijn 
    {
        health--;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }
}
