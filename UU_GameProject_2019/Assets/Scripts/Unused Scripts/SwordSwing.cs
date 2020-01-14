using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    private float attackTime = 0.33f;
    private float stopTime = 0.0f;
    public ProjectileLaunchScript Owner;

    public Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        //disables rendering of the sword before it is swung
        GetComponent<Renderer>().enabled = false;
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time > stopTime && GetComponent<Renderer>().enabled)
        {
            GetComponent<Renderer>().enabled = false;
            Owner.lootObject(Weapon.Sword, 1);
        }
    }

    public void SetOwner(ProjectileLaunchScript owner)
    {
        Owner = owner;
    }

    public void StartSwing()
    {
        GetComponent<Renderer>().enabled = true;
        updateTime();
        animator.Play("Sword Attack", 0, 0);
    }

    //times the duration of the sword attack
    void updateTime(){
        stopTime = Time.time + attackTime;
    }
}