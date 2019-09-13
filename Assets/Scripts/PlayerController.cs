using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{

    Vector3 movement;
    float moveX, moveZ;
    public float speed;
    PlayerBehaviour playerBehaviour;

    public enum State
    {
        Innocent = 1,
        Murderer = 2,
        Hidden = 3,
    }
    public State state;

    public enum PlayerNum
    {
        PlayerOne = 1,
        PlayerTwo = 2,
    }
    public PlayerNum playerNum;

    public void Start()
    {
        playerBehaviour = GetComponent<PlayerBehaviour>();
    }

    public void Update()
    {
        if (playerNum == PlayerNum.PlayerOne)
        {
            if (Input.GetKeyDown(KeyCode.E) && playerBehaviour.CloseObjects_List.Count > 0)
            {
                playerBehaviour.InteractionNPC();
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.I) && playerBehaviour.CloseObjects_List.Count > 0)
            {
                playerBehaviour.InteractionNPC();
            }
        }
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
        if (moveX != 0 || moveZ != 0)
        {
            GetComponent<Animator>().SetBool("moving", true);
        }
        else
        {
            GetComponent<Animator>().SetBool("moving", false);
        }


        Move(moveX, moveZ);
    }

    void Move(float movementX, float movementZ)
    {
        movement.Set(movementX, 0, movementZ);
        movement = movement.normalized;

        //transform.Translate(movement * speed * Time.deltaTime);
        gameObject.GetComponent<NavMeshAgent>().velocity = movement * speed;
    }


}
