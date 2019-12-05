using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomerangThrowing : MonoBehaviour
{
    public static Vector3 Destination;
    public GameObject BoomerangEmitter, BoomerangPrefab;
    public float Distance;
    public string boomerangButton;
    public bool throwing = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(boomerangButton) && !throwing)
        {
            GameObject Boomerang = Instantiate(BoomerangPrefab, BoomerangEmitter.transform.position, BoomerangEmitter.transform.rotation) as GameObject;
            Boomerang.transform.rotation = BoomerangPrefab.transform.rotation;
            throwing = true;
            Destination = BoomerangEmitter.transform.position + (transform.forward * Distance);
            Destroy(Boomerang, 5);
        }
    }
}