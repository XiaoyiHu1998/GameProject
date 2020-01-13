using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WizardCloneMovement : MonoBehaviour
{
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_animator = GetComponent<Animator>();
        m_animator.SetBool("idle_combat", true);
    }
}
