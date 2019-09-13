using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : StateMachine_Controller
{

    public int minTimeToDrink, maxTimeToDrink;
    public float timeToDrink, timer;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);
        timeToDrink = Random.Range(minTimeToDrink, maxTimeToDrink);
    }

    // OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if ((Vector3.Distance(Get_CharacterPosition(), Get_Target())) < Get_DetectionDistance())
        {
            timer += Time.deltaTime;
            if (timer > timeToDrink)
            {
                animator.SetBool("ToTheBar", false);
            }
        }
    }
}
