using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    public GameObject loseScreen;

    public void ShowLoseScreen(Transform playerkilled)
    {
        if (playerkilled.name == "PlayerOne")
        {
            loseScreen.transform.GetChild(1).GetComponent<Text>().text = "J2 WINS";
        }
        else
        {
            loseScreen.transform.GetChild(1).GetComponent<Text>().text = "J1 WINS";
        }

        Time.timeScale = 0;
        loseScreen.SetActive(true);
    }

    public void Start()
    {
        Time.timeScale = 1;
    }

    public void ExitApp()
    {
        Application.Quit();
    }

    public void ResetScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }
}
