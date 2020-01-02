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
        SetHealthText();
    }

    void OnCollisionEnter(Collision collisionInformation)
    {       
        //Check to see if the player collides with the enemy.
        if (collisionInformation.collider.tag == "EnemyTag")
        {  
            //Lower the players health but also update the health text (showcasing the current health on screen)
            Debug.Log("enemy hit");
            TakeDamage();
        }
    }

    void SetHealthText()
    {
        //HealthText.text = "Current health: " + playerHealth.ToString();
    }

    void Respawn()
    {
        player.transform.position = respawnPosition.transform.position;
        playerHealth = 6;
        //SceneManager.LoadScene("DeathScene");
        
    }

    public void TakeDamage()
    {
        playerHealth--;
        SetHealthText();

        if (playerHealth <= 0)
        {
            Debug.Log("You died.");
            Respawn();
        }
    }
}
