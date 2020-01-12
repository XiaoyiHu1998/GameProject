using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRightTile : MonoBehaviour
{
    public static bool rightTilePressed;
    protected Vector3 pressedPosition, originalPosition, originalArrowPosition;
    protected GameObject rightArrow;
    protected float speed;

    /// <summary>
    /// Checks if there is collision with the player and if so sets the pressed bool to true.
    /// </summary>
    /// <param name="RightTileCollision"></param>
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "ThirdPersonController" || collision.gameObject.name == "AIThirdPersonController")
        { rightTilePressed = true; }
    }
    
    // Resets tile bool.
    private void OnCollisionExit(Collision collision)
    {
        rightTilePressed = false;
    }

    // Setting initial variables.
    void Start()
    {
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        rightArrow = GameObject.Find("RightArrow");
        originalArrowPosition = new Vector3(rightArrow.transform.position.x, rightArrow.transform.position.y, rightArrow.transform.position.z);
        speed = 0.1f;
    }

    /// <summary>
    /// Checks if tile is pressed. If pressed it moves the tile and the arrow image on the tile downwards.
    /// If it is not pressed it stays on the starting position.
    /// </summary>
    void LateUpdate()
    {
       if (rightTilePressed)
        {
            if(Vector3.Distance(transform.position, pressedPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z), speed*Time.deltaTime);
                rightArrow.transform.position = Vector3.MoveTowards(rightArrow.transform.position, new Vector3(rightArrow.transform.position.x, rightArrow.transform.position.y - 0.15f, rightArrow.transform.position.z), speed*Time.deltaTime);
            }
        }
       else
        {
            transform.position = originalPosition;
            rightArrow.transform.position = originalArrowPosition;
        }
    }
}
