using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateMachine_Controller : StateMachineBehaviour
{
    public AI_Controller characterController;
    
    //Write here all functions we need to have access later
    //Could be more Useful to use a function "Start" -> to update all variables before doing anything else

    public AI_Controller Get_CharacterController(Animator animator)
    {
        if(characterController == null)
        {
            characterController = animator.GetComponent<AI_Controller>();
        }
        return characterController;
    }

    public AI_Controller.Role Get_CharacterRole(Animator animator)
    {
        if (characterController == null)
        {
            characterController = animator.GetComponent<AI_Controller>();
        }
        return characterController.role;
    }

    public AI_Controller.State Get_CharacterState(Animator animator)
    {
        if (characterController == null)
        {
            characterController = animator.GetComponent<AI_Controller>();
        }
        return characterController.state;
    }

    public Transform Get_DanceLocation()
    {
        return characterController.dancePosition;
    }

    public Transform Get_ToiletLocation()
    {
        return characterController.toiletPosition;
    }

    public Vector3 Get_Target()
    {
        return new Vector3(characterController.target.position.x, characterController.target.position.y, characterController.target.position.z);
    }

    public void Set_Target(Transform value)
    {
        characterController.target = value;
    }

    public NavMeshAgent Get_NavMeshAgent(Animator animator)
    {
        if (characterController == null)
        {
            characterController = animator.GetComponent<AI_Controller>();
        }
        return characterController.agent;
    }

    public void Set_CharacterState(AI_Controller.State value)
    {
        characterController.state = value;
    }


    public Vector3 Get_CharacterPosition()
    {
        return characterController.transform.position;
    }

    public float Get_DetectionDistance()
    {
        return characterController.detectionDistance;
    }

    public bool Get_CharacterDetected()
    {
        return (characterController.detection.visibleTargets.Count > 0);
    }

    public bool Get_State_Player()
    {
        //Will always go after the first player see
        GameObject playerDetected = characterController.detection.visibleTargets[0].gameObject;
        if (characterController.playersToWatch.Length > 0)
        {
            foreach (PlayerController w in characterController.playersToWatch)
            {
                if (playerDetected.name == w.name && w.state == PlayerController.State.Murderer)
                {
                    //Debug.Log("Murderer Detected");
                    Set_Target(playerDetected.transform);
                    return true;
                }
            }
        }
        return false; 
    }

    public void Get_OrientationPlayer()
    {
        characterController.transform.LookAt(Get_Target());
    }

    public void Set_Rotation(Quaternion value)
    {
        characterController.transform.rotation = value;
    }
}
