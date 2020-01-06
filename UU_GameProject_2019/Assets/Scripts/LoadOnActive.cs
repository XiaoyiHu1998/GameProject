using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadOnActive : MonoBehaviour
{
    public void OnEnable()
    {
        SceneManager.LoadScene("SpawnScene");
    }
}