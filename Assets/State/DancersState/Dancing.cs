using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancing : StateMachine_Controller
{
    public int minTimeToDance, maxTimeToDance;
    public float timeToDance, timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Debug.Log("I'm Dancing !");
        Get_CharacterController(animator);
        timeToDance =  Random.Range(minTimeToDance, maxTimeToDance);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        //Dance Animation
        timer += Time.deltaTime;
        if(timer > timeToDance)
        {
            //Stop Animation
            //Move to Another Spot or go to the bar or the Toilet

            //For Now it will be a simple Random
            int what_Do_I_Do = Random.Range(1, 5);
            //Debug.Log(what_Do_I_Do);
            switch(what_Do_I_Do)
            {
                case 1:
                   // Debug.Log("I'm Staying on this spot");
                    timeToDance = Random.Range(minTimeToDance, maxTimeToDance);
                    timer = 0;
                    break;
                case 2:
                    //Debug.Log("Hmm i'm gonna dance elsewhere");
                    //Change That
                    int index = Random.Range(0, characterController.dancePositions.Length);
                    Set_Target(characterController.dancePositions[index]);
                    Get_NavMeshAgent(animator).SetDestination(Get_Target());

                    animator.SetBool("isDancing", false);
                    animator.SetBool("isMoving", true);
                    Set_CharacterState(AI_Controller.State.Moving);
                    timer = 0;
                    break;
                case 3:
                    //Debug.Log("I need to drink");
                    Set_CharacterState(AI_Controller.State.Moving);

                    int indexToFind = Random.Range(0, characterController.barPositions.Length);
                    Set_Target(characterController.barPositions[indexToFind]);
                    Get_NavMeshAgent(animator).SetDestination(Get_Target());

                    animator.SetBool("isDancing", false);
                    animator.SetBool("isMoving", true);
                    animator.SetBool("ToTheBar", true);
                    timer = 0;
                    break;
                case 4:
                    //Debug.Log("I need to go to the toilet");
                    Set_CharacterState(AI_Controller.State.Moving);
                    Set_Target(Get_ToiletLocation());
                    animator.SetBool("isDancing", false);
                    animator.SetBool("isMoving", true);
                    animator.SetBool("ToToilet", true);
                    timer = 0;
                    break;
                default:
                    break;
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
