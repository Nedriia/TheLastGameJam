using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorObj : MonoBehaviour
{

    public float delayBetweenPress;
    public float delayBetweenPressTmp;
    GameObject firstPlayerPress;
    public Light ambientLight;
    public float powerIncreased;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (firstPlayerPress != null)
        {
            if (delayBetweenPressTmp > delayBetweenPress)
            {
                firstPlayerPress = null;
            }
            else
            {
                delayBetweenPressTmp += Time.deltaTime;
            }
        }

    }

    public void interaction(GameObject player)
    {
        if (firstPlayerPress == null)
        {
            firstPlayerPress = player;
            delayBetweenPressTmp = 0;
        }
        else
        {
            if (firstPlayerPress != player)
            {
                firstPlayerPress = null;
                ambientLight.intensity += powerIncreased;
                if (ambientLight.intensity > 2)
                {
                    ambientLight.intensity = 2;
                }
            }
        }

    }
}
