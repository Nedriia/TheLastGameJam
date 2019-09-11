using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing_KillerBHV : KillerStateMachine_Controller
{

    Transform target;
    public float chasingSpeed = 10;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        //Get the Transform of the First Player Viewed and Chase him
        if (killerController.PlayerinView()) {
            target = killerController.fieldOfView.visibleTargets[0];
            killerController.GetAgent().SetDestination(target.position);
            killerController.GetAgent().speed = chasingSpeed;

            animator.SetBool("isPatrolling", false);
        }
        else
        {
            animator.SetBool("isChasing", false);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (killerController.PlayerinView())
        {
            if (killerController.fieldOfView.visibleTargets[0].gameObject != target.gameObject)
            {
                target = null;
                animator.SetBool("isChasing", false);
            }
            else
            {
                killerController.GetAgent().SetDestination(target.position);

                if (Vector3.Distance(killerController.transform.position, target.position) < killerController.chasingDetectionDistance)
                {
                    Debug.Log("Player Chased!");
                }
            }
        }
        else
        {
            target = null;
            animator.SetBool("isChasing", false);
        }

    }
}
