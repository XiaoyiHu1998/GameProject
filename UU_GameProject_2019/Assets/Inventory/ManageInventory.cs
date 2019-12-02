using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManageInventory : MonoBehaviour
{
    public GameObject Item1, Item2, Item3, Item4, Item5;
    public GameObject Cursor;
    public int Selection { get; private set; }

    void Start()
    {
    }

    void Update()
    {
        if (Input.GetKeyDown("e")) SwitchItem(Selection+1);
        if (Input.GetKeyDown("q")) SwitchItem(Selection-1);
    }

    public void SwitchItem(int switchto)
    {
        if (switchto >= 0 && switchto <= 4)
        {
            Selection = switchto;
            GameObject[] ItemList = new GameObject[] { Item1, Item2, Item3, Item4, Item5 };
            foreach (GameObject o in ItemList)
            {
                o.SetActive(false);
            }
            ItemList[Selection].SetActive(true);
            Cursor.transform.localPosition = new Vector3(0, 200 - 100*Selection, 0);
        }
    }
}
