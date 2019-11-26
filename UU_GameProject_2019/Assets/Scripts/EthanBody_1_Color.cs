using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EthanBody_1_Color : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(UnityEngine.Input.GetKeyDown(KeyCode.Z))
        {
            GetComponent<Renderer>().enabled = false;
        }
        else if(UnityEngine.Input.GetKeyDown(KeyCode.X))
        {
            GetComponent<Renderer>().enabled = true;
        }
    }
}
