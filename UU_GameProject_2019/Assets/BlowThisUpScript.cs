using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlowThisUpScript : MonoBehaviour, IExplodable
{
    public void getExploded()
    {
        Destroy(gameObject);
    }
}