using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordSwing : MonoBehaviour
{
    private Animation animation;
    // Start is called before the first frame update
    void Start()
    {
            GetComponent<Renderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey(KeyCode.E)){
            GetComponent<Renderer>().enabled = true;
            animation.Play("swordSwing");
            animation.Stop();
            GetComponent<Renderer>().enabled = false;
            
        }
    }
}
