using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static SceneManager Instance { get; private set; } // static singleton
    // Declare any public variables that you want to be able 
    // to access throughout your scene

    public GameManager gameManager;
    public GameObject playerOne, playerTwo;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
        // Cache references to all desired variables
        if (gameManager == null) { gameManager = FindObjectOfType<GameManager>(); }
        if (playerOne == null) { playerOne = GameObject.Find("PlayerOne"); }
        if (playerTwo == null) { playerTwo = GameObject.Find("PlayerTwo"); }

    }
}
