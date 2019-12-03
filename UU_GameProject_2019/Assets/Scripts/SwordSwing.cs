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
            GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E) && !GetComponent<Renderer>().enabled){
            GetComponent<Renderer>().enabled = true;
            updateTime();
            animation.Play();
        }
        else if(Time.time > stopTime){
            GetComponent<Renderer>().enabled = false;
        }
    }

    void updateTime(){
        stopTime = Time.time + attackTime;
    }
}
