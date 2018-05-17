using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour {

    public AudioMixer audioMixer;

    // Accepts a difficulty, 0 easy, 1 normal, 2 hard
	public void PlayGame (int difficulty)
    {
        // Set game difficulty in game manager
        switch (difficulty)
        {
            case 0:
                GameManager.Instance.Difficulty = Difficulty.EASY;
                break;
            case 1:
                GameManager.Instance.Difficulty = Difficulty.NORMAL;
                break;
            case 2:
                GameManager.Instance.Difficulty = Difficulty.HARD;
                break;
            default:
                break;
        }
        
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame ()
    {
        Application.Quit();
    }

    /* Sets the volume of the master audio mixer by setting the passed in volume to the exposed parameter "volume" */
    public void SetVolume (float volume)
    {
        print(volume);
        audioMixer.SetFloat("volume", volume);
    }
}
