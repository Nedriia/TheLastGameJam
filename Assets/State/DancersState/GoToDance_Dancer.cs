using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToDance_Dancer : StateMachine_Controller
{

    public Vector3 wheretoDance;
    float distanceToStartDancing = 4f;
    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        if (characterController.name == "AI_Dancer")
        {
            Debug.Log("Enter");
        }
        // Where to Dance 
        int index = Random.Range(0, characterController.dancePositions.Length);
        wheretoDance = Outils.RandomPointInBounds(characterController.dancePositions[index].GetComponent<BoxCollider>().bounds);
        wheretoDance.y = 2;
        Get_NavMeshAgent(animator).SetDestination(wheretoDance);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (characterController.name == "AI_Dancer")
        {
            Debug.Log(wheretoDance);
            Debug.Log(Vector3.Distance(Get_CharacterPosition(), wheretoDance) + "/////" + distanceToStartDancing);
        }
        if (Vector3.Distance(Get_CharacterPosition(), wheretoDance) < distanceToStartDancing)
        {
            if (characterController.name == "AI_Dancer")
            { Debug.Log("END"); }
                
            animator.SetBool("isDancing", true);
            animator.SetBool("GoingToDance", false);
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
