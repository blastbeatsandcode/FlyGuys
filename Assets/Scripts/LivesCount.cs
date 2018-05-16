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

    // Update the number of lives in the HUD
    void Update()
    {
        numLives = GameManager.Instance.Lives;
        livesText.text = "LIVES x " + numLives.ToString();
    }
}