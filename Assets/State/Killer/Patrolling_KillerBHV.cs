using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling_KillerBHV : KillerStateMachine_Controller
{
    public List<Vector3> positionsToPatroll;
    // How many points inside of the Area to patroll is going to Go
    public int maxPositionsPatrolling, minPositionsPatrolling;
    public float detectionDistancePoints;
    // Delay of each Points to Patroll
    public float delayPoints;
    float delayPointsTmp = 0;
    bool checkingPoint = false;

    public float patrollingSpeed = 3.5f;

    public NavMeshPath navMeshPath;

    // OnStateEnter is called when a transition starts and the state machine starts to evaluate this state
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Get_CharacterController(animator);

        navMeshPath = new NavMeshPath();

        //Setting up the random points where the killer will go (inside a BoxCollider)
        positionsToPatroll = new List<Vector3>();
        int index = Random.Range(0, killerController.patrollingAreas.Length);
        int nulOfPoints = Random.Range(minPositionsPatrolling, maxPositionsPatrolling);
        for (int x = 0; x < nulOfPoints; x++) 
        {
            Vector3 newPoint = Outils.RandomPointInBounds(killerController.patrollingAreas[index].GetComponent<BoxCollider>().bounds);
            newPoint.y = killerController.transform.position.y;
            positionsToPatroll.Add(newPoint);
        }
        killerController.GetAgent().SetDestination(positionsToPatroll[0]);
        killerController.GetAgent().speed = patrollingSpeed;

    }

    //OnStateUpdate is called on each Update frame between OnStateEnter and OnStateExit callbacks
    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (positionsToPatroll.Count > 0 && !checkingPoint)
        {
            killerController.GetAgent().CalculatePath(positionsToPatroll[0], navMeshPath);
            if (navMeshPath.status == NavMeshPathStatus.PathComplete)
            {
                if (Vector3.Distance(killerController.transform.position, positionsToPatroll[0]) < detectionDistancePoints)
                {
                    checkingPoint = true;
                }
            }
            else
            {
                delayPointsTmp = 0;
                checkingPoint = false;
                positionsToPatroll.RemoveAt(0);
                if (positionsToPatroll.Count == 0)
                {
                    animator.SetBool("isPatrolling", false);
                }
                else
                {
                    killerController.GetAgent().SetDestination(positionsToPatroll[0]);
                }
            }
        }else if (positionsToPatroll.Count == 0 && !checkingPoint)
        {
            animator.SetBool("isPatrolling", false);
        }

        // A delay after the Killer arrives to the Points, after he goes to the next Point ( if they are no more point he search the next action )
        if (checkingPoint)
        {
            delayPointsTmp += Time.deltaTime;
            if (delayPointsTmp > delayPoints)
            {
                delayPointsTmp = 0;
                checkingPoint = false;
                positionsToPatroll.RemoveAt(0);
                if (positionsToPatroll.Count == 0)
                {
                    animator.SetBool("isPatrolling", false);
                }
                else
                {
                    killerController.GetAgent().SetDestination(positionsToPatroll[0]);
                }

            }
        }
    }

}
