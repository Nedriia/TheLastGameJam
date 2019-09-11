using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToPost : StateMachine_Controller
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        Set_Target(null);
        Get_NavMeshAgent(animator).SetDestination(characterController.post);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((Vector3.Distance(Get_CharacterPosition(), characterController.post)) > Get_DetectionDistance())
        {
            Get_NavMeshAgent(animator).SetDestination(characterController.post);
        }

        //Look for murderer
        if (Get_CharacterDetected())
        {
            if (CheckIfPlayerisMurderer())
            {
                animator.SetBool("MurdererFound", true);
                Set_CharacterState(AI_Controller.State.Chasing);
            }
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
