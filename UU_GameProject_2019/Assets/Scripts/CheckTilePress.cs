using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckTilePress : MonoBehaviour
{
    public static bool rightTilePressed;
    public static bool leftTilePressed;
    protected bool tilePress;
    protected GameObject player;
    protected Vector3 pressedPosition, originalPosition, originalArrowPosition;
    protected float speed;

    /// <summary>
    /// Checks if there is collision with the player and if so sets the pressed bool to true.
    /// </summary>
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        { tilePress = true; }
    }

    // Setting initial variables.
    void Start()
    {
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        speed = 0.1f;
        player = GameObject.Find("Player");
    }

    /// <summary>
    /// Checks if tile is pressed. If pressed it moves the tile and the arrow image on the tile downwards.
    /// If it is not pressed it stays on the starting position.
    /// </summary>
    void LateUpdate()
    {
       if (tilePress && (Vector3.Distance(transform.position, pressedPosition) > 0.1f) && (Vector3.Distance(transform.position, player.transform.position) < 2f))
       {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z), speed * Time.deltaTime);
            if(transform.position.x < 20) { leftTilePressed = true; }
            else { rightTilePressed = true; }
       }
    }
}
