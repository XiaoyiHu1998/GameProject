using UnityEngine;
using System.Collections;

public class BombLaunchScript : MonoBehaviour
{
    public GameObject BombEmitter;
    public GameObject Bomb;

    public float BombFuseTime;
    public Vector3 BombForce;

    void Start()
    {

    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            GameObject MyBomb = Instantiate(Bomb, BombEmitter.transform.position, BombEmitter.transform.rotation) as GameObject;
            MyBomb.GetComponent<Rigidbody>().AddRelativeForce(BombForce);
            Destroy(MyBomb, BombFuseTime);
        }
    }
}