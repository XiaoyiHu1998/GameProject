﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangScript : MonoBehaviour
{
    public float speed = 10f;
    float RotatingSpeed, timer;
    bool returning;
    public Vector3 Destination, playerRelativePos;
    public CharacterControl Owner;

    void Start()
    {
        returning = false;
        RotatingSpeed = 1024;
        timer = 8;
    }

    void Update()
    {
        Vector3 playerPos = GameObject.Find("Player").transform.position;
        playerRelativePos = new Vector3(playerPos.x, transform.position.y, playerPos.z);

        //rotates a 'RotatingSpeed' degrees per second around y axis
        transform.Rotate(0, 0, RotatingSpeed * Time.deltaTime, Space.Self); 

        if (returning)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerRelativePos, speed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, Destination, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, Destination) < 3)
            {
                returning = true;
            }
        }
        //if the boomerang glitches out, then the timer will reach zero. when it does it returns the boomerang to the player
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            Owner.inv.lootObject(Weapon.Boomerang, 1);
            Destroy(gameObject);
        }
    }

    public void SetDestination(Vector3 destination)
    {
        Destination = destination;
    }

    public void SetOwner(CharacterControl owner)
    {
        Owner = owner;
    }

    void OnCollisionEnter(Collision target)
    {
        IStunable stunable = target.gameObject.GetComponent<IStunable>();

        if (stunable != null)
        {
            stunable.getStunned();
            returning = true;
        }

        ItemDropScript lootDrop = target.gameObject.GetComponent<ItemDropScript>();

        if (lootDrop != null)
        {
            Owner.inv.lootObject(lootDrop.weapon, lootDrop.amount);
            Destroy(lootDrop.gameObject);
        }

        HealthDropScript healthDrop = target.gameObject.GetComponent<HealthDropScript>();

        if (healthDrop != null)
        {
            Owner.inv.lootHealth(healthDrop.amount);
            Destroy(healthDrop.gameObject);
        }

        MoneyDropScript moneyDrop = target.gameObject.GetComponent<MoneyDropScript>();

        if (moneyDrop != null)
        {
            Owner.inv.lootMoney(moneyDrop.amount);
            Destroy(moneyDrop.gameObject);
        }

        if (target.gameObject.tag == "Player" && returning) 
        {
            Owner.inv.lootObject(Weapon.Boomerang, 1);
            Destroy(gameObject);
        }

        else
        {
            returning = true;
        }
    }
}