using UnityEngine;
using System.Collections;

public class AnimatorControl : MonoBehaviour
{
    private Animator animator;
    private AnimatorStateInfo curStateInfo;
    public bool bPower = false;

    // Use this for initialization
    void Start()
    {
        animator = transform.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        curStateInfo = animator.GetCurrentAnimatorStateInfo(0);
        animator.SetInteger("Action", 0);

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (!bPower)
            {
                if ((curStateInfo.IsName("Attack.attack_1") || curStateInfo.IsName("Attack.attack_2") ||
                    curStateInfo.IsName("Attack.attack_3") || curStateInfo.IsName("Attack.attack_4")))
                {
                    return;
                }

                animator.SetInteger("Action", (int)EnumAction.Attack);
            }

            else
            {
                if (curStateInfo.IsName("PowerAttack.powerattack_1") || curStateInfo.IsName("PowerAttack.powerattack_2") ||
               curStateInfo.IsName("PowerAttack.powerattack_3") || curStateInfo.IsName("PowerAttack.powerattack_4") ||
               curStateInfo.IsName("PowerAttack.powerattack_5"))
                {
                    return;
                }

                animator.SetInteger("Action", (int)EnumAction.PowerAttack);
            }
        }

        else if (Input.GetKeyDown(KeyCode.K))
        {
            if(curStateInfo.IsName("Skill.skill_1")) return;
            animator.SetInteger("Action", (int)EnumAction.Skill_1);
        }

        else if (Input.GetKeyDown(KeyCode.L))
        {
            if (curStateInfo.IsName("Skill.skill_3")) return;
            animator.SetInteger("Action", (int)EnumAction.Skill_3);
        }

        else if (Input.GetKeyDown(KeyCode.H))
        {
            if (curStateInfo.IsName("Base Layer.rush")) return;
            animator.SetInteger("Action", (int)EnumAction.Rush);
        }
    }
}
