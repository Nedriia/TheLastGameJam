using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillerStateMachine_Controller : StateMachine_Controller
{
    public KillerController killerController;

    public new KillerController Get_CharacterController(Animator animator)
    {
        if (killerController == null)
        {
            killerController = animator.GetComponent<KillerController>();
        }
        return killerController;

    }
}
