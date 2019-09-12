using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class LockerObj : MonoBehaviour
{
    GameObject playerInside;
    float basePlayerSpeed;
    bool delayactive = false;

    public void interaction(GameObject player)
    {
        if (playerInside == null)
        {
            if (player.layer != 11)
            {
                player.transform.position = transform.position;
                basePlayerSpeed = player.GetComponent<PlayerController>().speed;
                player.GetComponent<PlayerController>().speed = 0;
                player.GetComponent<PlayerController>().state = PlayerController.State.Hidden;
                player.GetComponent<CapsuleCollider>().enabled = false;

                player.GetComponent<PlayerBehaviour>().lockerHidden = gameObject;

                playerInside = player;

                delayactive = true;
                StartCoroutine(delayCatch());
            }
        }
        else
        {
            if (player.layer == 11 || player != playerInside)
            {
                playerInside.transform.position = transform.position + Vector3.right;
                playerInside.GetComponent<PlayerController>().speed = basePlayerSpeed;
                playerInside.GetComponent<PlayerController>().state = PlayerController.State.Innocent;
                playerInside.GetComponent<CapsuleCollider>().enabled = true;

                playerInside.GetComponent<PlayerBehaviour>().lockerHidden = null;

                playerInside.layer = 8;
                playerInside = null;
            }
            else
            {
                if (!delayactive)
                {
                    player.transform.position = transform.position + Vector3.right * 2;
                    player.GetComponent<PlayerController>().speed = basePlayerSpeed;
                    player.GetComponent<PlayerController>().state = PlayerController.State.Innocent;
                    player.GetComponent<CapsuleCollider>().enabled = true;

                    player.GetComponent<PlayerBehaviour>().lockerHidden = null;

                    player.layer = 8;
                    playerInside = null;
                }
            }


        }

    }

    IEnumerator delayCatch()
    {
        yield return new WaitForSeconds(1);
        if (playerInside != null)
        {
            playerInside.layer = 0;
            
        }
        delayactive = false;

    }
}
