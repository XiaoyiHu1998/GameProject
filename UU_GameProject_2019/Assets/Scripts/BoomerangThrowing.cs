using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrowing : MonoBehaviour
{
    public GameObject BoomerangEmitter;
    public GameObject BoomerangPrefab;
    public Vector3 ThrowingForce;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            GameObject Boomerang = Instantiate(BoomerangPrefab, transform.position, transform.rotation) as GameObject;
            Rigidbody rb = Boomerang.GetComponent<Rigidbody>();
            rb.AddForce(ThrowingForce);
        }
    }
}