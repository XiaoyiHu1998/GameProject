using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckRightTile : MonoBehaviour
{

    public static bool rightTilePressed;
    protected Vector3 pressedPosition, originalPosition, originalArrowPosition;
    protected GameObject rightArrow;
    protected float speed;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "ThirdPersonController")
        { rightTilePressed = true; }
    }

    private void OnCollisionExit(Collision collision)
    {
        rightTilePressed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        rightArrow = GameObject.Find("RightArrow");
        originalArrowPosition = new Vector3(rightArrow.transform.position.x, rightArrow.transform.position.y, rightArrow.transform.position.z);
        speed = 0.1f;
    }

    // Update is called once per frame
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
