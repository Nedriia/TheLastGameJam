using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckingObj_KillerBHV : KillerStateMachine_Controller
{
    public float distanceDetection;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        Debug.Log(killerController.GetAgent());
        killerController.GetAgent().SetDestination(killerController.objToCheck.transform.position);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(killerController.transform.position, killerController.objToCheck.transform.position) < distanceDetection)
        {
            Debug.Log("OUT");
            killerController.objToCheck.GetComponent<LockerObj>().interaction(killerController.gameObject);
            killerController.GetAgent().velocity = Vector3.zero;
            animator.SetBool("isChecking", false);
        }
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
