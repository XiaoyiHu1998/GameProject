using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class EnableTimeline : MonoBehaviour
{
    public PlayableDirector playableDirector;
    public GameObject canvas;

    public void Play()
    {
            playableDirector.Play();
            canvas.SetActive(true);
    }
}
