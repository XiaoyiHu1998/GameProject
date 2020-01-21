using System;
using UnityEngine;
using UnityEngine.Playables;

public class RunSwordCutscene : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject sceneSword;

    void Start()
    {
        if (!InventoryStats.WeaponAcquired[(int)Weapon.Sword])
        {
            sceneSword.gameObject.GetComponent<Renderer>().enabled = true;
            playableDirector.Play();
        }
        else
        {
            sceneSword.gameObject.GetComponent<Renderer>().enabled = false;
        }
    }
}