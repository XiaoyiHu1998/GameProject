using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCloneMovement : WizardMovement
{
    private Animator m_animator;
    private bool attacking2;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetBool("idle_combat", true);
        attacking2 = false;
    }

    // Update is called once per frame
    void Update()
    {
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(Quaternion.LookRotation(relativePlayerPos - transform.position, Vector3.up).eulerAngles), Time.deltaTime * speed);
        if (attacking2 && !base.attacking)
        {
            attacking2 = false;
        }
        else if (base.attacking && !attacking2)
        {
            m_animator.SetBool("attack_short_001", false);
            base.ShootMagicBolt();
            attacking2 = true;
        }
    }

    void TakeDamage()
    {
        Debug.Log("Clone Hit");
        gameObject.SetActive(false);
    }

    public void getShot()
    {
        TakeDamage();
    }

    public void getExploded()
    {
        TakeDamage();
    }

    public void getStunned()
    {
        TakeDamage();
    }
}
