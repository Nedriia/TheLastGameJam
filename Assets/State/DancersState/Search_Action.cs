using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search_Action : StateMachine_Controller
{
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        //For Now it will be a simple Random
        int what_Do_I_Do = Random.Range(1, 6);
        //Debug.Log(what_Do_I_Do);
        switch (what_Do_I_Do)
        {
            case 1:
            case 2:
            case 3:
                // Go to Dance Somewhere ( can be the same spot )
                animator.SetBool("isDancing", true);
                break;
            case 4:
                //Going to the Bar
                Set_CharacterState(AI_Controller.State.Moving);

                int indexToFind = Random.Range(0, characterController.barPositions.Length);
                Set_Target(characterController.barPositions[indexToFind]);
                Get_NavMeshAgent(animator).SetDestination(Get_Target());

                animator.SetBool("ToTheBar", true);
                break;
            case 5:
                // Need to go to the Toilet
                Set_CharacterState(AI_Controller.State.Moving);
                Set_Target(Get_ToiletLocation());
                animator.SetBool("ToToilet", true);
                break;
            default:
                animator.SetBool("isDancing", true);
                break;
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }

    //OnStateExit is called when a transition ends and the state machine finishes evaluating this state
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
}
