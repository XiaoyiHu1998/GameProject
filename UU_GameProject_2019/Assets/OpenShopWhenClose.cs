using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenShopWhenClose : MonoBehaviour
{
    public GameObject shopManager;
    GameObject player;
    Vector3 playerPosition;
    ShopUIManagerScript shopManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        shopManagerScript = shopManager.gameObject.GetComponent<ShopUIManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        playerPosition = player.transform.position;
        if (Vector3.Distance(transform.position, playerPosition) < 4)
        {
            shopManagerScript.OpenShop();
        }
        else
        {
            shopManagerScript.CloseShop();
        }
    }
}
