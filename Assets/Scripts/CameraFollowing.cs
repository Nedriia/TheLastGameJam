using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowing : MonoBehaviour
{

    public GameObject playerOne, playerTwo;
    public GameObject testing;
    public Vector3 cameraOffset;
    public LayerMask layerMasks;

    public Vector3 rigthLimit, leftLimit;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(Vector3.Distance(cameraOffset, (playerOne.transform.position + playerTwo.transform.position) / 2));
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(leftLimit), out hit, Mathf.Infinity, layerMasks))
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(leftLimit) * hit.distance, Color.yellow);
            Debug.Log("Did Hit");
        }
        else
        {
            Debug.DrawRay(transform.position, transform.TransformDirection(leftLimit) * 1000, Color.white);
            Debug.Log("Nothing Hit");
        }
        transform.position = (playerOne.transform.position + playerTwo.transform.position) / 2 + cameraOffset;
        //Debug.Log(Camera.main.(new Vector2(Screen.currentResolution.width, Screen.currentResolution.height/2)));
        Debug.Log(Vector3.forward);
    }
}
