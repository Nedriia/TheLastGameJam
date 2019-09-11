using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchAction_KillerBHV : KillerStateMachine_Controller
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        // If there is a Player in the View he start Chasing him if not Just go to Patroll
        if (killerController.PlayerinView())
        {
            animator.SetBool("isChasing", true);
        }
        else
        {
            animator.SetBool("isPatrolling", true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    //override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    //{
    //    
    //}
}
