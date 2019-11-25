using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RabbitMovement : MonoBehaviour
{
    // Start is called before the first frame update

    private string[] m_buttonNames = new string[] { "Idle", "Run", "Dead" };

    private Animator m_animator;

    // Use this for initialization
    void Start()
    {

        m_animator = GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {
        m_animator.SetInteger("AnimIndex", 0);
        m_animator.SetTrigger("Next");
    }


}
