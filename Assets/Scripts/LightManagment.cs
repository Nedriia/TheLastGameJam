using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LightManagment : MonoBehaviour
{

    public Light ambientLight;
    public float decreaseSpeed;
    public GameObject uiGenerator;
    public Vector3 ui_offset;
    public GameObject generatorObj;

    // Start is called before the first frame update
    void Start()
    {
        uiGenerator.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (ambientLight.intensity > 0)
        {
            ambientLight.intensity -= Time.deltaTime * decreaseSpeed;
        }
        uiGenerator.transform.GetChild(0).GetComponent<Slider>().value = ambientLight.intensity;
        uiGenerator.transform.position = Camera.main.WorldToScreenPoint(generatorObj.transform.position + ui_offset);
    }
}
