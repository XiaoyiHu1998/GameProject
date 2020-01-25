using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    protected float timer;
    protected bool nextScene;
    protected int[] loopOrder;
    protected Vector3 playerLoopPosition;
    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public string sceneName;
    public PlayableDirector playableDirector;
    public PlayableDirector fadeIn;
    public GameObject tree;
    public int looping;
    
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.name == "Player")
        {
            nextScene = true;   
        }
    }

    void Start()
    {
        timer = 0;
        nextScene = false;
        loopOrder = new int[3]{2,3,1};
        PlayerStats.loopStage = 0;
        playerLoopPosition = new Vector3(21, 0, 4);
    }

    void LateUpdate()
    {
        if (nextScene)
        {
            Play();
            SavePlayer();
            timer += Time.deltaTime;
            if (timer > 1)
            {
                if (looping < 1)
                {
                    SceneManager.LoadScene(sceneName: sceneName);
                }
                else
                {
                    GameObject.Find("Player").transform.position = playerLoopPosition;
                    fadeIn.Play();
                    HandleLoop();
                }
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

    void HandleLoop()
    {
        if (looping == loopOrder[PlayerStats.loopStage])
        {
            PlayerStats.loopStage += 1;
            tree.SetActive(false);
            if (PlayerStats.loopStage > loopOrder.Length - 1)
            {
                SavePlayer();
                SceneManager.LoadScene(sceneName: sceneName);
            }
        }
        else
        {
            PlayerStats.loopStage = 0;
            tree.SetActive(true);
        }
        nextScene = false;
    }
}
