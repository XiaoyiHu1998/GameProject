using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveToPlayer : MonoBehaviour
{

    float speed, rotationSpeed;
    // Start is called before the first frame update
    void Start()
    {
        speed = 1;
        rotationSpeed = 3;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerPosition = GameObject.Find("ThirdPersonController").transform.position;
        if(Vector3.Distance(transform.position,playerPosition) < 4)
        {
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, speed * Time.deltaTime);
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(playerPosition - transform.position, Vector3.up).eulerAngles), Time.deltaTime * rotationSpeed);
        }

    }
}
