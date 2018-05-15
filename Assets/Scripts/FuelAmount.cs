using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FuelAmount : MonoBehaviour
{

    public float fuelAmt;
    public TextMeshProUGUI fuelText;
    //GameObject gameManager;

    private void Start()
    {
        fuelAmt = 100.0f;
    }

    // Set level number to active scene
    void Update()
    {
       //TODO: actually get the fuel amount
        fuelText.text = "FUEL LEVEL " + fuelAmt.ToString("F2");
    }
}
