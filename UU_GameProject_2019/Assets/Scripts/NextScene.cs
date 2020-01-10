using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    protected float timer;
    protected bool nextScene;
    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public string sceneName;
    public PlayableDirector playableDirector;
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "ThirdPersonController" || collision.gameObject.name == "AIThirdPersonController")
        {
            nextScene = true;   
        }
    }

    void Start()
    {
        timer = 0;
        nextScene = false;
    }

    void Update()
    {
        if (nextScene)
        {
            Play();
            SavePlayer();
            timer += Time.deltaTime;
            if (timer > 1)
            {
                SceneManager.LoadScene(sceneName: sceneName);
            }
        }
    }

    void Play()
    {
        playableDirector.Play();
    }

    void SavePlayer()
    {
        PlayerStats.playerPosition = playerPosition;
        PlayerStats.playerRotation = playerRotation;
    }
}
