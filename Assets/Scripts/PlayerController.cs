using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    Vector3 movement;
    float moveX, moveZ;
    public float speed;

    public enum State
    {
        Innocent = 1,
        Murderer = 2,
    }
    public State state;

    // Update is called once per frame
    void FixedUpdate()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
        Move(moveX, moveZ);
    }

    void Move(float movementX, float movementZ)
    {
        movement.Set(movementX, 0, movementZ);
        movement = movement.normalized;

        transform.Translate(movement * speed * Time.deltaTime);
    }
}
