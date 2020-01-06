using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnableTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            playableDirector.Play();
        }
    }
}
