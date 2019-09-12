using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonObj : MonoBehaviour
{

    public GameObject porteref;
    public float baseAngle;
    public float currentAngle;
    public float offsetAngle;
    bool opened = false;
    bool moving = false;
    public float rotationSpeed = 40;

    // Start is called before the first frame update
    void Start()
    {
        baseAngle = porteref.transform.eulerAngles.y;
        currentAngle = baseAngle;
    }

    public void interaction()
    {
        if (!moving)
        {
            moving = true;
            
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (moving)
        {
            if (!opened)
            {
                if (((baseAngle - 90) - currentAngle) > 0)
                {
                    Debug.Log("Opened Door");
                    opened = true;
                    moving = false;
                }
                else
                {
                    currentAngle -= Time.deltaTime * rotationSpeed;
                    porteref.transform.eulerAngles = new Vector3(porteref.transform.eulerAngles.x, currentAngle, porteref.transform.eulerAngles.z);
                }
            }
            else
            {
                if ((baseAngle - currentAngle) < 0)
                {
                    opened = false;
                    moving = false;
                }
                else
                {
                    currentAngle += Time.deltaTime * rotationSpeed;
                    porteref.transform.eulerAngles = new Vector3(porteref.transform.eulerAngles.x, currentAngle, porteref.transform.eulerAngles.z);
                }
            }
        }
    }
}
