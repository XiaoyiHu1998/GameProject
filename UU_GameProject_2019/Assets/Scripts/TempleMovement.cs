using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleMovement : MonoBehaviour
{
    protected Vector3 spawnPosition, hidePosition;
    protected GameObject sand;
    protected float delta, speed;
    public bool tilesPressed;
    // Start is called before the first frame update
    void Start()
    {
        spawnPosition = transform.position;
        hidePosition = new Vector3(transform.position.x, transform.position.y - delta, transform.position.z);
        delta = 8;
        speed = 1;
        tilesPressed = false;
        sand = GameObject.Find("EntranceSand");
    }

    // Update is called once per frame
    void Update()
    {
        if (CheckLeftTile.leftTilePressed && CheckRightTile.rightTilePressed) { tilesPressed = true; }
        if (tilesPressed) { SetToSpawnPosition(); SwitchCamera.activeCamera = "secondCamera"; ; }
        else { SetToHidePosition(); SwitchCamera.activeCamera = "mainCamera"; }

        if (Vector3.Distance(spawnPosition, transform.position) < 0.1f) { SwitchCamera.activeCamera = "mainCamera"; }
    }

    protected void SetToSpawnPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnPosition, speed * Time.deltaTime);
        sand.transform.position = Vector3.MoveTowards(sand.transform.position, new Vector3(sand.transform.position.x, 0, sand.transform.position.z), speed * Time.deltaTime);
    }

    protected void SetToHidePosition()
    {
        if (Vector3.Distance(transform.position, hidePosition) < 0.2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - delta, transform.position.z); 
        }
    }
}
