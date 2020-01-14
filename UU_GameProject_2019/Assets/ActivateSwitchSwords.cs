using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSwitchSwords : MonoBehaviour
{
    public GameObject sceneSword;
    public GameObject playerSword;


    public void OnEnable()
    {
        sceneSword.SetActive(false);
        playerSword.SetActive(true);
    }

}
