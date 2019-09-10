using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VigilMoving : StateMachine_Controller
{

    public float basicSpeed, sprintSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        if (Get_CharacterDetected() && Get_State_Player())
        {
            animator.GetComponent<NavMeshAgent>().speed = sprintSpeed;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Get_CharacterDetected() && Get_State_Player())
        {
            Get_NavMeshAgent(animator).SetDestination(Get_Target());
            Get_OrientationPlayer();
            Set_CharacterState(AI_Controller.State.Moving);
        }
        else
        {
            Get_NavMeshAgent(animator).SetDestination(characterController.post);
            animator.GetComponent<NavMeshAgent>().speed = basicSpeed;
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
