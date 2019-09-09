using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeployServer : StateMachine_Controller
{
    public int minTimeToServe, maxTimeToServe;
    public float timeToServe, timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        int index = Random.Range(0, characterController.dancePositions.Length);
        Set_Target(characterController.dancePositions[index]);
        Get_NavMeshAgent(animator).SetDestination(Get_Target());
        timeToServe = Random.Range(minTimeToServe, maxTimeToServe);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((Vector3.Distance(Get_CharacterPosition(), Get_Target())) < Get_DetectionDistance())
        {
            timer += Time.deltaTime;
            if (timer > timeToServe)
            {
                //Stop Animation
                //Move to Another Spot or go to the bar or the Toilet
                animator.SetBool("isServing", true);
                Set_CharacterState(AI_Controller.State.Occupied);
                timer = 0;
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
