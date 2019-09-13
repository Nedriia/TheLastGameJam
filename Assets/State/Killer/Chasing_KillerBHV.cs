using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chasing_KillerBHV : KillerStateMachine_Controller
{

    public Transform target;
    Vector3 lastPos;
    public float chasingSpeed = 10;
    public bool CheckingLastPos = false;
    public bool RighSideChecked = false;
    public float baseRotation;
    public float currentAngle;
    public float rotationangle;
    public float rotationSpeed = 0.1f;

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
            animator.SetBool("isCheckingObj", false);
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
            if (killerController.fieldOfView.visibleTargets[0].GetComponent<PlayerController>().state == PlayerController.State.Hidden)
            {
                Debug.Log("DIFERENT PLAYER ><");
                killerController.objToCheck = killerController.fieldOfView.visibleTargets[0].GetComponent<PlayerBehaviour>().lockerHidden;
                animator.SetBool("isCheckingObj", true);
                animator.SetBool("isChasing", false);
            }
            if (target != null)
            {
                if (killerController.fieldOfView.visibleTargets[0].gameObject != target.gameObject && !CheckingLastPos)
                {

                    target = null;
                    animator.SetBool("isChasing", false);
                }
                else
                {
                    lastPos = target.position;
                    killerController.GetAgent().SetDestination(target.position);

                    if (Vector3.Distance(killerController.transform.position, target.position) < killerController.chasingDetectionDistance)
                    {
                        Debug.Log("Player Chased!");
                    }
                }
            }
        }

        if (!killerController.PlayerinView() && !CheckingLastPos)
        {
            CheckingLastPos = true;
            baseRotation = killerController.transform.rotation.eulerAngles.y;
            currentAngle = baseRotation;
            killerController.GetAgent().SetDestination(lastPos);
        }


        if (CheckingLastPos)
        {
            if (killerController.PlayerinView())
            {
                CheckingLastPos = false;
                RighSideChecked = false;
                animator.SetBool("isChasing", false);
            }
            else
            {
                if (Vector3.Distance(killerController.transform.position, lastPos) < 0.3f)
                {
                    if (!RighSideChecked)
                    {

                        if (((baseRotation + 90) - currentAngle) < 0)
                        {
                            RighSideChecked = true;
                        }
                        else
                        {
                            currentAngle += Time.deltaTime * rotationSpeed;
                            killerController.transform.eulerAngles = new Vector3(killerController.transform.eulerAngles.x, currentAngle, killerController.transform.eulerAngles.z);
                        }
                    }

                }
                else
                {
                    killerController.GetAgent().SetDestination(lastPos);
                }

                if (RighSideChecked)
                {
                    if (((baseRotation - 90) - currentAngle) > 0)
                    {
                        target = null;
                        animator.SetBool("isChasing", false);
                        CheckingLastPos = false;
                        RighSideChecked = false;
                    }
                    else
                    {
                        currentAngle -= Time.deltaTime * rotationSpeed;
                        killerController.transform.eulerAngles = new Vector3(killerController.transform.eulerAngles.x, currentAngle, killerController.transform.eulerAngles.z);
                    }
                }
            }
        }

    }


}
