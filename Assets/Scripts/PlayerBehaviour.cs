using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{

    public List<GameObject> CloseObjects_List;
    public GameObject closestObjectInteract;

    public List<GameObject> PlayersUI;


    // Update is called once per frame
    void Update()
    {
        UpdatePlayersUI();
    }

    public void UpdatePlayersUI()
    {
        if (CloseObjects_List.Count > 0)
        {
            foreach (GameObject objectInteract in CloseObjects_List)
            {
                if (objectInteract != closestObjectInteract)
                {
                    if (Vector3.Distance(objectInteract.transform.position, transform.position) < Vector3.Distance(closestObjectInteract.transform.position, transform.position))
                    {
                        closestObjectInteract.GetComponent<Outline>().enabled = false;
                        closestObjectInteract = objectInteract;
                        closestObjectInteract.GetComponent<Outline>().enabled = true;
                    }
                }

            }
        }

        if (closestObjectInteract != null)
        {
            PlayersUI[0].SetActive(true);
            Vector3 ui_offset = closestObjectInteract.gameObject.GetComponent<MeshRenderer>().bounds.size;
            ui_offset.y = 0;
            ui_offset.x = ui_offset.x / 2;
            PlayersUI[0].transform.position = Camera.main.WorldToScreenPoint(closestObjectInteract.transform.position + ui_offset);
        }
        else
        {
            PlayersUI[0].SetActive(false);
        }

    }

    public void InteractionNPC()
    {
        /*if (closestObjectInteract.GetComponent<AI_Controller>().role == AI_Controller.Role.Dancer)
        {
            GameObject killedNpc = closestObjectInteract;
            killedNpc.GetComponent<AI_Controller>().Set_CharacterState(AI_Controller.State.Dead);
            killedNpc.GetComponent<Animator>().SetBool("isDead", true);
            killedNpc.layer = 10;
            GetComponent<PlayerController>().state = PlayerController.State.Murderer;
        }*/
        closestObjectInteract.GetComponent<InteractableObjs>().activeItem();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "InterctObj")
        {
            CloseObjects_List.Add(other.gameObject);
            if (CloseObjects_List.Count == 1)
            {
                closestObjectInteract = other.gameObject;
                other.gameObject.GetComponent<Outline>().enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "InterctObj")
        {
            CloseObjects_List.Remove(other.gameObject);
            if (closestObjectInteract == other.gameObject)
            {
                closestObjectInteract.GetComponent<Outline>().enabled = false;
                closestObjectInteract = null;
            }
        }
    }
}
