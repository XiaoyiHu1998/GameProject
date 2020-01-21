using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMarkerPosition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.localPosition = PlayerStats.questMarkerPosition;
        
    }
}
