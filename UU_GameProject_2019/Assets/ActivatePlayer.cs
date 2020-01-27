using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivatePlayer : MonoBehaviour
{
    public GameObject player;

    private void OnEnable()
    {
        player.SetActive(true);
    }
}
