using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnableTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject canvas;
    // Update is called once per frame
    public void Play()
    {
       // if (Input.GetKeyDown(KeyCode.Space))

            playableDirector.Play();
            canvas.SetActive(true);

    }
}
