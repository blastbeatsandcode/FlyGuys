using System;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class Rocket : MonoBehaviour {
    // Hold rigid body of the rocket ship
    private Rigidbody rigidbody;
    private AudioSource audio;
    private State state;
    private bool collisionsEnabled = false;

    private enum State {Alive, Dying, Transcending}

    [Header("Level Settings")]

    [Tooltip("Sets the speed of fuel depletion. Defaults to 7.")]
    [SerializeField] int depletionRate = 7;

    [Tooltip("Set the starting fuel amount for the rocket. Defaults to 100f.")]
    [SerializeField] float fuelAmount = 100f;

    [Header("Physics Settings")]

    [Tooltip("Speed of left or right rotation")]
    [SerializeField] float rotationSpeed = 100f;

    [Tooltip("Thrust speed of rocket")]
    [SerializeField] float thrustSpeed = 100f;


    [Header("Audio Settings")]

    [Tooltip("Audio for engine sound")]
    [SerializeField] AudioClip engineSound;

    [Tooltip("Audio for explosion sound")]
    [SerializeField] AudioClip explosionSound;

    [Tooltip("Audio for Success")]
    [SerializeField] AudioClip finishSound;

    [Tooltip("Audio for Winning the game")]
    [SerializeField] AudioClip winSound;


    [Header("Particle Settings")]

    [Tooltip("Particles on thrust")]
    [SerializeField] ParticleSystem thrusterParticles;

    [Tooltip("Particles on death")]
    [SerializeField] ParticleSystem explosionParticles;

    [Tooltip("Particles for win")]
    [SerializeField] ParticleSystem winParticles;

    [Header("Misc. Settings")]

    [Tooltip("Sets delay between levels. Defaults to 2f.")] float levelLoadDelay = 2f;


    // Use this for initialization
    void Start () {
        // Set GameManager information for amount of fuel and fuel depletion of the rocket
        GameManager.Instance.Fuel = fuelAmount;
        GameManager.Instance.DepletionRate = depletionRate;

        // Grab rigidbody component from gameobject
        this.rigidbody = GetComponent<Rigidbody>();

        // Grab audio source for rocket
        this.audio = GetComponent<AudioSource>();

        // By default, the rocket's state is alive
        this.state = State.Alive;
	}
	
	// Update is called once per frame
	void Update () {
        // If the player runs out of fuel, die
        if (this.state == State.Alive)
        {
            RespondToThrustInput();
            RespondToRotateInput();
            if (GameManager.Instance.Fuel <= 0)
            {
                HandleDying(); // If the player has run out of fuel, die 
            }
        }

        // If it is a debug build, respond to the debug keys
        if (Debug.isDebugBuild)
        {
            RespondToDebugKeys();
        }
	}

    /* Respond to debug keys while in debug mode */
    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L)) // If key is L, advance to next level
        {
            LoadNextScene();
        }
        else if (Input.GetKeyDown(KeyCode.C)) // If the key is C, toggle collisions
        {
            collisionsEnabled = !collisionsEnabled;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if rocket is anything other than alive; if so, don't react to any other collisions
        if (this.state != State.Alive || collisionsEnabled) return;

        switch(collision.gameObject.tag)
        {
            case "Friendly":
                break;
            case "Pickup":
                print("encountered pickup; might not ever occur because pickups are usually triggers");
                break;
            case "Finish":
                HandleFinish();
                break;
            default: // Everything else makes us blow up
                HandleDying();
                break;
        }
    }

    private void HandleFinish()
    {
        this.state = State.Transcending;
        if (SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1))
        {
            print("You're the best pilot in all of space! You win!");
            thrusterParticles.Stop();
            winParticles.Play();
            audio.PlayOneShot(winSound);
            Invoke("LoadFirstScene", levelLoadDelay + 2f);
            return;
        }
        else
        {
            audio.PlayOneShot(finishSound);
        }

        Invoke("LoadNextScene", levelLoadDelay);    // Invoke LoadNextScene as coroutine with 1 second delay
    }

    private void HandleDying()
    {
        state = State.Dying;
        audio.PlayOneShot(explosionSound);
        thrusterParticles.Stop(); // Stop thruster particles
        explosionParticles.Play();
        GameManager.Instance.Lives--; // Decrease number of lives in GameManager
        Invoke("RestartLevel", levelLoadDelay);
    }

    private  void RestartLevel()
    {
        print("Exploded!");
        if (GameManager.Instance.Lives == 0)
        {
            // If the player runs out of lives, go to menu screen
            // TODO: Create game over screen
            SceneManager.LoadScene(0);
            print("Game Over!");
            GameObject.Destroy(GameManager.Instance); // delete the game manager to restart fresh
            return;
        }
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex)); // Restart on current scene
    }

    private void LoadNextScene()
    {
        // continue to the next level by grabbing current level index and incrementing it
        // Check if last level, then show win screen
        if (SceneManager.GetActiveScene().buildIndex == (SceneManager.sceneCountInBuildSettings - 1))
        {
            HandleFinish();
        }
        SceneManager.LoadScene((SceneManager.GetActiveScene().buildIndex) + 1);
        print("Finished!");
    }

    /* Load the first scene. */
    private void LoadFirstScene()
    {
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
       // if (other.gameObject.CompareTag("Pick Up"))
       // {
        //    GameObject.Destroy(other.gameObject);
       // }
    }

    private void RespondToThrustInput()
    {
        // Check to see if spacebar is being pressed
        if (Input.GetKey(KeyCode.Space))
        {
            ApplyThrust();
        }
        else
        {
            audio.Stop();
            thrusterParticles.Stop();
        }
    }

    private void ApplyThrust()
    {
        // Relative force will move the object in the direction specified relative to where it's currently positioned
        float thrustThisFrame = thrustSpeed * Time.deltaTime;
        rigidbody.AddRelativeForce(Vector3.up * thrustThisFrame);
        GameManager.Instance.Fuel -= (depletionRate * Time.deltaTime); // Reduce fuel

        // Play audio if it is not already playing
        if (!audio.isPlaying)
        {
            this.audio.PlayOneShot(engineSound);
        }
        thrusterParticles.Play();
    }

    private void RespondToRotateInput()
    {
        this.rigidbody.freezeRotation = true; // Take manual control of rotation; eliminates physics from 

        float rotationThisFrame = rotationSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.A))
        {
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }

        this.rigidbody.freezeRotation = false;  // Resume physics control
    }
}