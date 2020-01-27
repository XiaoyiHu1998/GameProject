using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadStats : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        CharacterController cc = GetComponent<CharacterController>();
        cc.enabled = false;
        transform.position = PlayerStats.playerPosition;
        transform.rotation = Quaternion.Euler(PlayerStats.playerRotation);
        cc.enabled = true;
    }
}
