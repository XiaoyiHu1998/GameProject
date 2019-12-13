using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    protected bool nextScene;
    protected bool loadedScene;
    public string sceneName;

    /// <summary>
    /// Checks if there is collision with the edge of the level.
    /// The object that this script is assigned to is the edge of the level.
    /// Sets next scene true if there is collision.
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "ThirdPersonController" || collision.gameObject.name == "AIThirdPersonController")
        { nextScene = true; }
    }

    // Setting initial variables.
    void Start()
    {
        nextScene = false;
        loadedScene = false;
    }

    // Grabs the desiged scene name and loads that scene.
    void Update()
    {
        if(nextScene && !loadedScene)
        {
            SceneManager.LoadScene(sceneName: sceneName);
            loadedScene = true;
        }

    }
}
