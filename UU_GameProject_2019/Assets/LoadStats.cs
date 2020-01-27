using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.position = PlayerStats.playerPosition;
        transform.rotation = Quaternion.Euler(PlayerStats.playerRotation);
    }
}
