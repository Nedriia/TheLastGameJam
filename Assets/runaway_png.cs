using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class runaway_png : MonoBehaviour
{

    public float delayInScreen;
    public bool active = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (delayInScreen > 0)
        {
            delayInScreen -= Time.deltaTime;
        }
        else
        {
            if (active)
            {
                active = false;
                gameObject.SetActive(false);
            }
        }
    }
}
