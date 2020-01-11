using UnityEngine;
using System.Collections;

public class AniPlay : MonoBehaviour
{
    public Transform[] transforms;
    public GUIContent[] GUIContents;
    private Animator[] animator;
    private string currentState = "";

    void Start()
    {
        animator = new Animator[transforms.Length];
        for (int i = 0; i < transforms.Length; i++)
        {
            animator[i] = transforms[i].GetComponent<Animator>();
        }
    }

    void OnEventFx(GameObject InEffect)
    {
        GameObject newSpell = Instantiate(InEffect);

        Destroy(newSpell, 1.0f);
    }

    void OnGUI()
    {
        GUILayout.BeginVertical("box");
        for (int i = 0; i < GUIContents.Length; i++)
        {

            if (GUILayout.Button(GUIContents[i]))
            {
                currentState = GUIContents[i].text;
            }

            AnimatorStateInfo stateInfo = animator[0].GetCurrentAnimatorStateInfo(0);

            if (!stateInfo.IsName("Base Layer.idle"))
            {
                for (int j = 0; j < animator.Length; j++)
                {
                    animator[j].SetBool("Standing", false);
                    animator[j].SetBool("Walking", false);
                    animator[j].SetBool("Running", false);
                    animator[j].SetBool("Stunned", false);
                    animator[j].SetBool("Victory", false);
                }
            }

            if (currentState != "")
            {
                if (stateInfo.IsName("Base Layer.walk") && currentState != "walk")
                {
                    for (int j = 0; j < animator.Length; j++)
                    {
                        animator[j].SetBool("Walking", false);
                    }
                }

                if (stateInfo.IsName("Base Layer.run") && currentState != "run")
                {
                    for (int j = 0; j < animator.Length; j++)
                    {
                        animator[j].SetBool("Running", false);
                    }
                }

                if (stateInfo.IsName("Base Layer.die") && currentState != "die")
                {
                    for (int j = 0; j < animator.Length; j++)
                    {
                        animator[j].SetTrigger("Revive");
                    }
                }

                switch (currentState)
                {

                    case "stand":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetBool("Standing", true);
                        }

                        break;
                    case "walk":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetBool("Walking", true);
                        }

                        break;
                    case "run":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetBool("Running", true);
                        }
                        break;
                    case "jump":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetTrigger("Jump");
                        }
                        break;
                    case "damage":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetTrigger("TakeDamage");
                        }
                        break;
                    case "stun":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetBool("Stunned", true);
                        }
                        break;
                    case "attack01":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetTrigger("Attack");
                        }
                        break;

                    case "attack02":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetTrigger("ShieldBash");
                        }

                        break;
                    case "attack03":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetTrigger("JumpSlash");
                        }
                        break;

                    case "attack04":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetTrigger("SwordThrust");
                        }

                        break;
                    case "win":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetBool("Victory", true);
                        }
                        break;
                    case "die":
                        for (int j = 0; j < animator.Length; j++)
                        {
                            animator[j].SetTrigger("Die");
                        }
                        break;

                    default:
                        break;
                }
                currentState = "";
            }
        }
        GUILayout.EndVertical();
    }



}
