using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShoot : StateMachineBehaviour
{
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public float timer;
    private float WT;
    public float SWT;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Star", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Star", false);
        if (timer <= 0)
        {

            animator.SetTrigger("Teleport");
            animator.SetBool("Star", false);
        }
        else
        {
            timer -= Time.deltaTime;
        }
        if (WT <= 0)
        {
            animator.transform.position = new Vector2(Random.Range(minX, maxX), Random.Range(minY, maxY));
            WT = SWT;
        }
        else
        {
            WT -= Time.deltaTime;
        }
    }

    // OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

}

