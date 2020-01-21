using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActive : MonoBehaviour
{
    public Vector3 playerPosition;
    public Vector3 playerRotation;

    public void OnEnable()
    {
        SceneManager.LoadScene("ShoopBookScene");
    }
}