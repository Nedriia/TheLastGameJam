using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Action : StateMachine_Controller
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("Idle State, Looking for Something to do");
        //Debug.Log(Get_CharacterRole(animator).ToString());

        //Is looking for dancing
        Get_CharacterController(animator);
        int index = Random.Range(0, characterController.dancePositions.Length);
        Set_Target(characterController.dancePositions[index]);
        Get_NavMeshAgent(animator).SetDestination(Get_Target());

        Set_CharacterState(AI_Controller.State.Moving);
        animator.SetBool("isMoving", true);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

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
