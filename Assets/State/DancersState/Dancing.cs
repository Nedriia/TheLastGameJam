using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dancing : StateMachine_Controller
{
    public int minTimeToDance, maxTimeToDance;
    public float timeToDance, timer;
    float distanceToStartDancing = 1;
    bool dancing = false;
    Vector3 wheretoDance;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);

        // Where to Dance 
        int index = Random.Range(0, characterController.dancePositions.Length);
        wheretoDance = Outils.RandomPointInBounds(characterController.dancePositions[index].GetComponent<BoxCollider>().bounds);
        wheretoDance.y = 0;
        Get_NavMeshAgent(animator).SetDestination(wheretoDance);
        dancing = false;
        
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Vector3.Distance(Get_CharacterPosition(), wheretoDance) < distanceToStartDancing && !dancing)
        {
            timeToDance = Random.Range(minTimeToDance, maxTimeToDance);
            dancing = true;
        }
        //Dance Animation
        if (dancing)
        {
            timer += Time.deltaTime;
            //Stop Dancing, Find Something to Do
            if (timer > timeToDance){
                animator.SetBool("isDancing", false);
            }
        }
    }
}
