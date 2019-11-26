using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempleMovement : MonoBehaviour
{
    protected Vector3 spawnPosition, hidePosition;
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
    }

    // Update is called once per frame
    void Update()
    {
        if(CheckLeftTile.leftTilePressed && CheckRightTile.rightTilePressed) { tilesPressed = true; }

        if (tilesPressed) { SetToSpawnPosition(); }
        else { SetToHidePosition(); }
    }

    protected void SetToSpawnPosition()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnPosition, speed * Time.deltaTime);
    }

    protected void SetToHidePosition()
    {
        if (Vector3.Distance(transform.position, hidePosition) < 0.2f)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y - delta, transform.position.z); 
        }
    }
}
