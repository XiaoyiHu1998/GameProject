using UnityEngine;
using System.Collections;

public class BombLaunchScript : MonoBehaviour
{
    public GameObject BombEmitter;
    public GameObject Bomb;
    
    public Vector3 BombForce; //X is naar voren, Y is omhoog, Z is als je voor whatever reden de bomb emitter ergens scheef op hebt geplakt

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("space")) //zit tijdelijk op Space omdat Ethan's spring animatie het beste is wat ik op een gooi animatie vond lijken
        {
            GameObject MyBomb = Instantiate(Bomb, BombEmitter.transform.position, BombEmitter.transform.rotation) as GameObject;
            MyBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
        }
    }
}