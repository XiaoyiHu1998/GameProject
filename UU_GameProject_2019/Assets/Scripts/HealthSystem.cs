using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HealthSystem : MonoBehaviour
{
    public int playerHealth = 6;
    public Text HealthText;
    //public string DeathScene;
    //Scene currentScene, SpawnScene;

    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPosition;

    void Start()
    {
        Debug.Log("hallo");
        SetHealthText();
    }

    void OnCollisionEnter(Collision collisionInformation)
    {       

        if (collisionInformation.collider.tag == "DoosTag")
        {  
            playerHealth--;

            SetHealthText();

            Debug.Log("New health = " + playerHealth);

            if (playerHealth <= 0)
            {
                Debug.Log("You died.");
                Respawn();
                
            }
        }
        
    }

    void SetHealthText()
    {
        HealthText.text = "Current health: " + playerHealth.ToString();
    }

   /* void Update()
    {
        currentScene = SceneManager.GetActiveScene();

        if (Input.GetKeyDown("space") && currentScene.name == "SpawnScene")
        {
            SceneManager.LoadScene("SpawnScene");
            Debug.Log("Je bent momenteel in de spawn scene");
        }
    }*/

    void Respawn()
    {
        player.transform.position = respawnPosition.transform.position;
        playerHealth = 6;
        //SceneManager.LoadScene("DeathScene");
        
    }



    
}
