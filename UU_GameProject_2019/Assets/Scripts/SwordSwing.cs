using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    [SerializeField] private string attack_KeyBinding;
    private float attackTime = 0.33f;
    private float stopTime = 0.0f;

    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
            //disables rendering of the sword before it is swung
            GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //checks if sword needs to be swung, before turning on the rendering and playing aniation
        if(Input.GetKey(attack_KeyBinding) && !GetComponent<Renderer>().enabled){
            GetComponent<Renderer>().enabled = true;
            updateTime();
            animation.Play();
        }
        else if(Time.time > stopTime){
            GetComponent<Renderer>().enabled = false;
        }
    }

    //times the duration of the sword attack
    void updateTime(){
        stopTime = Time.time + attackTime;
    }
}
