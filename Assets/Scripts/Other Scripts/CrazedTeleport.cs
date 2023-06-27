using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazedTeleport : StateMachineBehaviour
{
    public float timer;
    private float WT;
    public float SWT;
    private int rand;
    public float minX;
    public float maxX;
    public float minY;
    public float maxY;
    public GameObject Knife;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rand = Random.Range(5,9);
        if(rand >= 8)
        {
            animator.SetTrigger("Star");
        }
        timer = rand;
        WT = SWT;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (timer <= 0)
        {
            animator.SetTrigger("Duplic");
            
        }        
        else
        {
            timer -= Time.deltaTime;
        }
        if (WT <= 0)
        {
            Instantiate(Knife, animator.transform.position, Quaternion.identity);
            Instantiate(Knife, animator.transform.position, Quaternion.identity);
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
