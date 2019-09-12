using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjs : MonoBehaviour
{
    public enum Typeof
    {
        Button = 1,
        Loker = 2,
    }
    public Typeof typeOf;

    public void activeItem(GameObject player)
    {
        switch (typeOf)
        {
            case Typeof.Button:
                gameObject.GetComponent<ButtonObj>().interaction();
                break;
            case Typeof.Loker:
                gameObject.GetComponent<LockerObj>().interaction(player);
                break;
            default:
                break;
        }
    }
}
