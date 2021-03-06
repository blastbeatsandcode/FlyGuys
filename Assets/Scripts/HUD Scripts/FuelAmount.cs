﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FuelAmount : MonoBehaviour
{

    public float fuelAmt;
    public TextMeshProUGUI fuelText;
    //GameObject gameManager;


    // Set level number to active scene
    void Update()
    {
        if (GameManager.Instance.Difficulty != Difficulty.EASY)
        {
            fuelAmt = GameManager.Instance.Fuel;
            // Adjust the fuel amount so that it doesn't show a negative number
            if (fuelAmt <= 0.0f) fuelAmt = 0.0f;
            fuelText.text = "FUEL LEVEL " + fuelAmt.ToString("F2");
        }
        else
        {
            fuelText.text = "FUEL LEVEL MAX";
        }
    }
}
