using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjs : MonoBehaviour
{
    public enum Typeof
    {
        Innocent = 1,
        Murderer = 2,
    }
    public Typeof typeOf;

    public void activeItem()
    {
        Debug.Log("Button Active!");
    }
}
