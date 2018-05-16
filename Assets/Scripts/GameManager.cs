using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public float Fuel { get; set; } // Amount of fuel rocket has to start level
    public int Lives { get; set; }  // Amount of lives rocket has left
    public int DepletionRate { get; set; } // Rate of depletion of the fuel

    public static GameManager Instance
    {
        get
        {
            // create logic to create the instance if not exists
            if (_instance == null)
            {
                GameObject go = new GameObject("GameManager");
                go.AddComponent<GameManager>();
            }

            return _instance;
        }
    }

    void Awake()
    {
        _instance = this;
        DontDestroyOnLoad(this); // Keep the game manager consistent
    }

    void Start()
    {
        // Start out with 5 lives
        Lives = 5;
    }
}
