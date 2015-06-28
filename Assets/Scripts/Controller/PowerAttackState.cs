using UnityEngine;
using System.Collections;

public class PowerAttackState : StateMachineBehaviour
{
    public override void OnStateMachineEnter(Animator animator, int stateMachinePathHash)
    {
        base.OnStateMachineEnter(animator, stateMachinePathHash);

       // animator.SetInteger("Action", 0);
    }

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

        if (Input.GetKeyDown(KeyCode.K))
        {
            if (stateInfo.IsName("PowerAttack.powerattack_1") && stateInfo.normalizedTime > 0.35f)
            {
                animator.CrossFade("powerattack_2", 0);
            }

            else if (stateInfo.IsName("PowerAttack.powerattack_2") && stateInfo.normalizedTime > 0.35f)
            {
                animator.CrossFade("powerattack_3", 0);
            }

            else if (stateInfo.IsName("PowerAttack.powerattack_3") && stateInfo.normalizedTime > 0.35f)
            {
                animator.CrossFade("powerattack_4", 0);
            }

            else if (stateInfo.IsName("PowerAttack.powerattack_4") && stateInfo.normalizedTime > 0.35f)
            {
                animator.CrossFade("powerattack_5", 0);
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
