using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LevelCount : MonoBehaviour {

    public int level;
    public TextMeshProUGUI levelText;
	
	// Set level number to active scene
	void Start () {
        level = SceneManager.GetActiveScene().buildIndex;
        levelText.text = "LEVEL " + level.ToString();
	}
}
