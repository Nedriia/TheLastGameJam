using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Panic : StateMachine_Controller
{

    public float minwalkRadius, maxwalkRadius;
    public float baseSpeed, sprintSpeed;
    Vector3 finalPosition;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        if (animator.GetComponent<AI_Controller>().state != AI_Controller.State.Panic)
        {
            Set_CharacterState(AI_Controller.State.Panic);

            //Evade -> Try to Find Random Point to Escape
            float walkRadius = Random.Range(minwalkRadius, maxwalkRadius);
            Vector3 randomDirection = Random.insideUnitSphere * walkRadius;

            randomDirection += animator.gameObject.transform.position;
            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, walkRadius, 1);
            finalPosition = hit.position;

            Get_NavMeshAgent(animator).SetDestination(finalPosition);
            Get_NavMeshAgent(animator).speed = sprintSpeed;

            // UI
            animator.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((Vector3.Distance(Get_CharacterPosition(), finalPosition)) < Get_DetectionDistance())
        {
            int index = Random.Range(0, characterController.dancePositions.Length);
            Set_Target(characterController.dancePositions[index]);
            Get_NavMeshAgent(animator).SetDestination(Get_Target());
            animator.transform.GetChild(0).gameObject.SetActive(false);

            Set_CharacterState(AI_Controller.State.Moving);
            Get_NavMeshAgent(animator).speed = baseSpeed;
            animator.SetBool("isPanicking", false);
            animator.SetBool("isMoving", true);

        }
    }

}
