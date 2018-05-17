using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;

    public float Fuel { get; set; } // Amount of fuel rocket has to start level
    public int Lives { get; set; }  // Amount of lives rocket has left
    public int DepletionRate { get; set; } // Rate of depletion of the fuel
    public string DebugSelection { get; set; } // Sets the value of the debug text on the HUD

    public Difficulty Difficulty;

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
        // Start out with 10 lives
        Lives = 10;
    }
}

// Define difficulty settings
public enum Difficulty { EASY, NORMAL, HARD }