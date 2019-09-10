using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector3 movement;
    float moveX, moveZ;
    public float speed;

    public List<GameObject> CloseNPCs_List;

    public enum State
    {
        Innocent = 1,
        Murderer = 2,
    }
    public State state;

    public enum PlayerNum
    {
        PlayerOne = 1,
        PlayerTwo = 2,
    }
    public PlayerNum playerNum;

    public void Update()
    {
        if (playerNum == PlayerNum.PlayerOne)
        {
            if (Input.GetKeyDown(KeyCode.E) && CloseNPCs_List.Count > 0)
            {
                KillingNPC();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I) && CloseNPCs_List.Count > 0)
            {
                KillingNPC();
            }
        }
    }

    public void KillingNPC()
    {
        /*GameObject killedNpc = CloseNPCs_List[0];
        CloseNPCs_List.RemoveAt(0);
        Destroy(killedNpc);*/
        GameObject killedNpc = CloseNPCs_List[0];
        killedNpc.GetComponent<AI_Controller>().Set_CharacterState(AI_Controller.State.Dead);
        killedNpc.GetComponent<Animator>().SetBool("isDead", true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerNum == PlayerNum.PlayerOne)
        {
            moveX = Input.GetAxis("Horizontal");
            moveZ = Input.GetAxis("Vertical");
        }
        else
        {
            moveX = Input.GetAxis("Horizontal2");
            moveZ = Input.GetAxis("Vertical2");
        }

        Move(moveX, moveZ);
    }

    void Move(float movementX, float movementZ)
    {
        movement.Set(movementX, 0, movementZ);
        movement = movement.normalized;

        transform.Translate(movement * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "NPC")
        {
            CloseNPCs_List.Add(other.gameObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "NPC")
        {
            CloseNPCs_List.Remove(other.gameObject);
        }
    }
}
