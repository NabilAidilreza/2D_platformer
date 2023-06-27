using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Defaulter : StateMachineBehaviour
{
    private int rand;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Change", false);

        rand = Random.Range(0, 8);
        if (rand <= 2)
        {
            animator.SetTrigger("ChangeBugger");
            animator.SetBool("Change", true);
        }
        else if (rand > 2 && rand <= 4)
        {
            animator.SetTrigger("ChangeSpammer");
            animator.SetBool("Change", true);
        }
        else if (rand > 4 && rand <= 5)
        {
            animator.SetTrigger("ChangeBeamer");
            animator.SetBool("Change", true);
        }
        else if(rand > 6 && rand <= 7)
        {
            animator.SetTrigger("ChangeFaller");
            animator.SetBool("Change", true);
        }
        else
        {
            animator.SetBool("Rest", true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    //override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}

    // OnStateMove is called right after Animator.OnAnimatorMove()
    //override public void OnStateMove(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that processes and affects root motion
    //}

    // OnStateIK is called right after Animator.OnAnimatorIK()
    //override public void OnStateIK(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    // Implement code that sets up animation IK (inverse kinematics)
    //}
}
