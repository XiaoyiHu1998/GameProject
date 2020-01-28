using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HealthScript : MonoBehaviour
{
    public Vector3 RespawnPosition;
    public Quaternion RespawnRotation;

    public int MaxHealth; //here for testing purposes, move to static file with all constants before handing in
    public int CurrentHealth;

    public float lastHit;
    public float hitDuration;

    public GameObject HealthBar;
    private Animator anim;
    private CharacterControl charcontrol;

    void Start()
    {
        anim = GetComponent<Animator>();
        charcontrol = GetComponent<CharacterControl>();

        RespawnPosition = PlayerStats.playerPosition;

        RespawnRotation = Quaternion.Euler(PlayerStats.playerRotation);

        updateHealthBar();
    }

    public void TakeDamage() //call this from the enemy's script on collision
    {
        if (Time.time - hitDuration >= lastHit)
        {
            anim.SetTrigger("TakeDamage");
            charcontrol.unmovableTimer = 1.0f;

            lastHit = Time.time;
            PlayerStats.playerHealth--;
            if (PlayerStats.playerHealth <= 0)
                Respawn();
            updateHealthBar();
        }
    }

    void Respawn()
    {
        SceneManager.LoadScene(sceneName: SceneManager.GetActiveScene().name);
        PlayerStats.playerHealth = PlayerStats.maxHealth;
    }

    public void updateHealthBar()
    {
        HealthBar.transform.localScale = new Vector3(PlayerStats.playerHealth, 1, 1);
    }
}