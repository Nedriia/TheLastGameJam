using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI_Controller : MonoBehaviour
{
    public enum Role
    {
        Dancer = 1,
        Server = 2,
        Medic = 3,
        Vigil = 4,
        Target = 5,
    }

    public enum State
    {
        Dead = 1,
        Moving = 2,
        Panic = 3,
        Dancing = 4,
        Occupied =5,
    }

    [Header("AI States")]
    public Role role;
    public State state;

    //Transform
    [Header("Locations for Dancers")]
    public Transform dancePosition;
    public Transform toiletPosition;

    public Transform[] toiletPositions;
    public Transform[] dancePositions;
    public Transform[] barPositions;

    public Transform target;
    public NavMeshAgent agent;

    [Header("Locations for Vigils")]
    public Vector3 post;
    public PlayerController[] playersToWatch;

    [Header("Location For Servers")]
    public Transform barLocation;

    [Header("Variables to Adjust")]
    public float detectionDistance = 5;

    [Header("Detection")]
    public FieldOfView detection;


    // Start is called before the first frame update
    void Start()
    {
        post = transform.position;
        if(GetComponent<Animator>() == null)
        {
            Animator animator = gameObject.AddComponent<Animator>();
            detection = GetComponent<FieldOfView>();
            switch (role)
            {
                //Loading Dancer Animation
                case Role.Dancer:
                    animator.runtimeAnimatorController = Resources.Load("AI_AnimatorController_Dancer") as RuntimeAnimatorController;
                    break;
                case Role.Vigil:
                    animator.runtimeAnimatorController = Resources.Load("AI_AnimatorController_Vigil") as RuntimeAnimatorController;
                    break;
                case Role.Server:
                    animator.runtimeAnimatorController = Resources.Load("AI_AnimatorController_Server") as RuntimeAnimatorController;
                    break;
                default:
                    role = Role.Dancer;
                    animator.runtimeAnimatorController = Resources.Load("AI_AnimatorController_Dancer") as RuntimeAnimatorController;
                    break;
            }
        }

        if(GetComponent<NavMeshAgent>() == null)
        {
            NavMeshAgent nav_agent = gameObject.AddComponent<NavMeshAgent>();
            agent = nav_agent;
        }
    }

    public void Set_CharacterState(State value)
    {
        state = value;
    }
}
