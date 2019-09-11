using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class VigilChasing : StateMachine_Controller
{

    public float basicSpeed, sprintSpeed;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        animator.GetComponent<NavMeshAgent>().speed = sprintSpeed;
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(characterController.transform.position, Get_Target()) < Get_VigilCatchDistance())
        {
            SceneManager.Instance.gameManager.ShowLoseScreen();
        }
        if (Get_CharacterDetected() && CheckIfPlayerisMurderer())
        {
            Get_NavMeshAgent(animator).SetDestination(Get_Target());
            Get_OrientationPlayer();
        }
        else
        {
            animator.GetComponent<NavMeshAgent>().speed = basicSpeed;
            animator.SetBool("MurdererFound", false);
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
