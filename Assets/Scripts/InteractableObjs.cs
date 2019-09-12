using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjs : MonoBehaviour
{
    public enum Typeof
    {
        Button = 1,
        Other = 2,
    }
    public Typeof typeOf;

    public void activeItem()
    {
        switch (typeOf)
        {
            case Typeof.Button:
                gameObject.GetComponent<ButtonObj>().interaction();
                break;
            default:
                break;
        }
    }
}
