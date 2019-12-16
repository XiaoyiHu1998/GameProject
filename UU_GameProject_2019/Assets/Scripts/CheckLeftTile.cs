using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckLeftTile : MonoBehaviour
{

    public static bool leftTilePressed;
    protected Vector3 pressedPosition, originalPosition, originalArrowPosition;
    protected GameObject leftArrow;
    protected float speed;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.name == "ThirdPersonController" || collision.gameObject.name == "AIThirdPersonController")
        { leftTilePressed = true; }
    }

    private void OnCollisionExit(Collision collision)
    {
        leftTilePressed = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        originalPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
        pressedPosition = new Vector3(transform.position.x, transform.position.y - 0.2f, transform.position.z);
        leftArrow = GameObject.Find("LeftArrow");
        originalArrowPosition = new Vector3(leftArrow.transform.position.x, leftArrow.transform.position.y, leftArrow.transform.position.z);
        speed = 0.1f;
    }

    // Update is called once per frame
    void LateUpdate()
    {
       if (leftTilePressed)
        {
            if(Vector3.Distance(transform.position, pressedPosition) > 0.1f)
            {
                transform.position = Vector3.MoveTowards(transform.position, new Vector3(transform.position.x, transform.position.y - 0.15f, transform.position.z), speed*Time.deltaTime);
                leftArrow.transform.position = Vector3.MoveTowards(leftArrow.transform.position, new Vector3(leftArrow.transform.position.x, leftArrow.transform.position.y - 0.15f, leftArrow.transform.position.z), speed*Time.deltaTime);
            }
        }
       else
        {
            transform.position = originalPosition;
            leftArrow.transform.position = originalArrowPosition;
        }
    }
}
