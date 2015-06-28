using UnityEngine;
using System.Collections;

public class AttackState : StateMachineBehaviour
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);

      //  animator.SetInteger("Action", 0);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);      
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (Input.GetKeyDown(KeyCode.J))
        {
            if (stateInfo.IsName("Attack.attack_1") && stateInfo.normalizedTime > 0.35f)
            {
                animator.CrossFade("attack_2", 0);
            }

            else if (stateInfo.IsName("Attack.attack_2") && stateInfo.normalizedTime > 0.35f)
            {
                animator.CrossFade("attack_3", 0);
            }

            else if (stateInfo.IsName("Attack.attack_3") && stateInfo.normalizedTime > 0.35f)
            {
                animator.CrossFade("attack_4", 0);
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }

    public override void OnStateMachineExit(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineExit(animator, stateMachinePathHash);
    } 
}
