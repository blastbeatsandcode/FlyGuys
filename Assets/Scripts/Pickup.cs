using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour {

    [Tooltip("Amount to increase fuel. Defaults to 25f.")]
    [SerializeField] float amountFuel = 25f;

    // Use this for initialization
    private void OnTriggerEnter(Collider other)
    {
        GameManager.Instance.Fuel += amountFuel;
    }
}
