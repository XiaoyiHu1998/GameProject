using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActive : MonoBehaviour
{
    public Vector3 playerPosition;
    public Vector3 playerRotation;
    public string sceneName;

    public void OnEnable()
    {
        SceneManager.LoadScene(sceneName: sceneName);
    }
}