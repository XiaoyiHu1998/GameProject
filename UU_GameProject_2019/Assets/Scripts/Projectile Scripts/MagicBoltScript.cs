using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBoltScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.tag == "Player")
        {
            HealthSystem Script = GameObject.FindObjectOfType(typeof(HealthSystem)) as HealthSystem;
            Script.TakeDamage();
            Debug.Log("Player");
        }

        Destroy(gameObject);
        Debug.Log("Destroy");
    }
}
