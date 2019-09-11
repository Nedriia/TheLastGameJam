using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KillerController : MonoBehaviour
{

    [Header("Detection")]
    public FieldOfView fieldOfView;
    public PlayerController[] playersToWatch;

    [Header("Patrolling Vars")]
    public Transform[] patrollingAreas;

    [Header("Variables to Adjust")]
    public float AgentAcceleration = 8;
    public float chasingDetectionDistance = 0.5f;

    NavMeshAgent agent;

    // Start is called before the first frame update
    void Start()
    {
        if (GetComponent<Animator>() == null)
        {
            Animator animator = gameObject.AddComponent<Animator>();
            fieldOfView = GetComponent<FieldOfView>();
            animator.runtimeAnimatorController = Resources.Load("AI_AnimatorController_Killer") as RuntimeAnimatorController;
        }

        if (GetComponent<NavMeshAgent>() == null)
        {
            NavMeshAgent nav_agent = gameObject.AddComponent<NavMeshAgent>();
            nav_agent.acceleration = AgentAcceleration;
            agent = nav_agent;
        }
        if (playersToWatch == null)
        {
            playersToWatch = new PlayerController[2] { SceneManager.Instance.playerOne.GetComponent<PlayerController>(), SceneManager.Instance.playerTwo.GetComponent<PlayerController>() };
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerinView())
        {
            GetComponent<Animator>().SetBool("isChasing", true);
        }
    }

    public NavMeshAgent GetAgent()
    {
        return agent;
    }

    public bool PlayerinView()
    {
        if (fieldOfView.visibleTargets.Count > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
