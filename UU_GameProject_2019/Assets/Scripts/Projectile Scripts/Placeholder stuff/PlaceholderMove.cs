using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceholderMove : MonoBehaviour
{
    void Update()
    {
        transform.Translate(Input.GetAxis("Vertical") * 2 * Time.deltaTime, 0f, 0f, Space.Self);
        transform.Rotate(0f, Input.GetAxis("Horizontal") * 120 * Time.deltaTime, 0f);
    }
}
