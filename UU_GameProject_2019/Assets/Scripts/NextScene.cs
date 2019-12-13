using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextScene : MonoBehaviour
{
    protected bool nextScene;
    protected bool loadedScene;
    public string sceneName;

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.name == "ThirdPersonController" || collision.gameObject.name == "AIThirdPersonController")
        { nextScene = true; }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextScene = false;
        loadedScene = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(nextScene && !loadedScene)
        {
            SceneManager.LoadScene(sceneName: sceneName);
            loadedScene = true;
        }

    }
}
