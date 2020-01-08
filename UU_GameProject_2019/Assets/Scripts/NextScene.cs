using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    protected bool nextScene;
    protected bool loadedScene;
    public string sceneName;
    public PlayableDirector playableDirector;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "ThirdPersonController" || collision.gameObject.name == "AIThirdPersonController")
        { nextScene = true; }
    }

    void Start()
    {
        nextScene = false;
        loadedScene = false;
    }

    void Update()
    {
        if(nextScene && !loadedScene)
        {
            Play();
            SceneManager.LoadScene(sceneName: sceneName);
            loadedScene = true;
        }

    }

    void Play()
    {
        playableDirector.Play();
    }
}
