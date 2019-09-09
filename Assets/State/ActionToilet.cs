using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionToilet : StateMachine_Controller
{
    public int minTimeToWait, maxTimeToWait;
    public float timeToWait, timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        int index = Random.Range(0, characterController.toiletPositions.Length);
        Set_Target(characterController.toiletPositions[index]);
        Get_NavMeshAgent(animator).SetDestination(Get_Target());
        timeToWait = Random.Range(minTimeToWait, maxTimeToWait);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((Vector3.Distance(Get_CharacterPosition(), Get_Target())) < Get_DetectionDistance())
        {
            timer += Time.deltaTime;
            if (timer > timeToWait)
            {
                //Stop Animation
                //Move to Another Spot or go to the bar or the Toilet

                //For Now it will be a simple Random
                int what_Do_I_Do = Random.Range(1, 4);
                //Debug.Log(what_Do_I_Do);
                switch (what_Do_I_Do)
                {
                    case 1:
                        //Debug.Log("I need to drink");
                        Set_CharacterState(AI_Controller.State.Moving);

                        int indexToFind = Random.Range(0, characterController.barPositions.Length);
                        Set_Target(characterController.barPositions[indexToFind]);
                        Get_NavMeshAgent(animator).SetDestination(Get_Target());

                        Get_NavMeshAgent(animator).SetDestination(Get_Target());
                        animator.SetBool("isOccupied", false);
                        animator.SetBool("isMoving", true);
                        animator.SetBool("ToTheBar", true);
                        animator.SetBool("ToToilet", false);
                        timer = 0;
                        break;
                    case 2:
                        //Debug.Log("Hmm i'm gonna dance at another spot");
                        Set_CharacterState(AI_Controller.State.Moving);

                        int index = Random.Range(0, characterController.dancePositions.Length);
                        Set_Target(characterController.dancePositions[index]);
                        Get_NavMeshAgent(animator).SetDestination(Get_Target());

                        animator.SetBool("isOccupied", false);
                        animator.SetBool("isMoving", true);
                        animator.SetBool("ToToilet", false);
                        timer = 0;
                        break;

                    case 3:
                        //Debug.Log("I'm gonna dance at the same spot");
                        Set_CharacterState(AI_Controller.State.Moving);

                        Set_Target(Get_DanceLocation());
                        Get_NavMeshAgent(animator).SetDestination(Get_Target());

                        animator.SetBool("isOccupied", false);
                        animator.SetBool("isMoving", true);
                        animator.SetBool("ToToilet", false);

                        timer = 0;
                        break;

                    default:
                        break;
                }
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
