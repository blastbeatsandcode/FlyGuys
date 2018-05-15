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

public enum Difficulty { EASY, MEDIUM, HARD }