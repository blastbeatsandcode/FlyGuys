using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LivesCount : MonoBehaviour
{

    public int numLives;
    public TextMeshProUGUI livesText;
    //GameObject gameManager;

    private void Start()
    {
        numLives = 5;
    }

    // Set level number to active scene
    void Update()
    {
        //TODO: actually get the fuel amount
        livesText.text = "LIVES x" + numLives.ToString();
    }
}