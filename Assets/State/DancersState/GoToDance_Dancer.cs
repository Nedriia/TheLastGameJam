﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GoToDance_Dancer : StateMachine_Controller
{

    public Vector3 wheretoDance;
    float distanceToStartDancing = 4f;

    public NavMeshPath navMeshPath;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);

        navMeshPath = new NavMeshPath();
        // Where to Dance 
        int index = Random.Range(0, characterController.dancePositions.Length);
        wheretoDance = Outils.RandomPointInBounds(characterController.dancePositions[index].GetComponent<BoxCollider>().bounds);
        wheretoDance.y = 2;
        Get_NavMeshAgent(animator).SetDestination(wheretoDance);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        if (Vector3.Distance(Get_CharacterPosition(), wheretoDance) < distanceToStartDancing)
        {             
            animator.SetBool("isDancing", true);
            animator.SetBool("GoingToDance", false);
        }
        else
        {
            Get_NavMeshAgent(animator).CalculatePath(wheretoDance, navMeshPath);
            if (navMeshPath.status != NavMeshPathStatus.PathComplete)
            {
                int index = Random.Range(0, characterController.dancePositions.Length);
                wheretoDance = Outils.RandomPointInBounds(characterController.dancePositions[index].GetComponent<BoxCollider>().bounds);
                wheretoDance.y = 2;
                Get_NavMeshAgent(animator).SetDestination(wheretoDance);
            }
        }
    }
}
