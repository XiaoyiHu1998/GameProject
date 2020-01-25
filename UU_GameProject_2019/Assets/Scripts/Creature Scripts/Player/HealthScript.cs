using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthScript : MonoBehaviour
{
    public Vector3 RespawnPosition;
    public Quaternion RespawnRotation;

    public int MaxHealth; //here for testing purposes, move to static file with all constants before handing in
    public int CurrentHealth;

    public float lastHit;
    public float hitDuration;

    public GameObject HealthBar;
    private Animator anim;
    private CharacterControl charcontrol;

    void Start()
    {
        anim = GetComponent<Animator>();
        charcontrol = GetComponent<CharacterControl>();

        if (!RetrieveStaticPosition())
            RespawnPosition = transform.position; //use default value if import was unsuccesful

        if (!RetrieveStaticRotation())
            RespawnRotation = transform.rotation;

        if (!RetrieveStaticHealth())
            CurrentHealth = MaxHealth;

        updateHealthBar();
    }

    public void TakeDamage() //call this from the enemy's script on collision
    {
        if (Time.time - hitDuration >= lastHit)
        {
            anim.SetTrigger("TakeDamage");
            charcontrol.unmovableTimer = 1.0f;

            lastHit = Time.time;
            CurrentHealth--;
            if (CurrentHealth == 0)
                Respawn();
            updateHealthBar();
        }
    }

    void Respawn()
    {
        transform.position = RespawnPosition;
        transform.rotation = RespawnRotation;
        CurrentHealth = MaxHealth;
    }

    void updateHealthBar()
    {
        HealthBar.transform.localScale = new Vector3(CurrentHealth, 1, 1);
    }

    bool RetrieveStaticPosition()
    {
        bool importComplete = false;
        //receive the location for this scene entry from the static quest/location script here
        //RespawnPosition = <yourcodehere>
        return importComplete;
    }

    bool RetrieveStaticRotation()
    {
        bool importComplete = false;
        //receive the rotation for this scene entry from the static quest/location script here
        //RespawnRotation = <yourcodehere>
        return importComplete;
    }

    bool RetrieveStaticHealth()
    {
        bool importComplete = false;
        //receive the player's curent health from the static quest/location script here
        //CurrentHealth = <yourcodehere>
        return importComplete;
    }
}