using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(AudioSource))]
public class CollisionsHandler : MonoBehaviour {
    private AudioSource audio;

    // object we want to collide with.
    [Header("Collision Settings")]

    [Tooltip("Object we want to collide with. Defaults to Player tag")]
    [SerializeField] private GameObject collidingObject;

    [Tooltip("Check if this object should be deleted on collide with colliding object")]
    [SerializeField] private bool deleteOnCollide = false;

    [Tooltip("Check if this object is a pickup item and toggles trigger on collider component")]
    [SerializeField] private bool isPickup = false;

    [Tooltip("Amount of time to delay before completely deleting the pickup; defaults to 3f. Change this if pickup audio needs more time to execute.")]
    [SerializeField] private float deleteDelay = 3f;


    // Set sounds for item pickups
    [Header("Audio Settings")]

    [Tooltip("Check to play audio on pickup")]
    [SerializeField] private bool playAudio = false;

    [Tooltip("Audio to play on pickup")]
    [SerializeField] private AudioClip pickupSound;

    // Use this for initialization
    void Start () {
        // Get instance of player and audio source
        this.collidingObject = GameObject.FindGameObjectWithTag("Player");
        this.audio = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        HandleTriggerSettings();
    }

    /* If we have checked isPickup, force collider to be a trigger */
    private void HandleTriggerSettings()
    {
        if (isPickup)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
        else
        {
            gameObject.GetComponent<Collider>().isTrigger = false;
        }
    }

    // Handle Trigger events
    private void OnTriggerEnter(Collider other)
    { 
        // Grab the root of each game object's transform so we don't compare children collisions
        if (other.transform.root == this.collidingObject.transform.transform.root)
        {
            print(other.gameObject + " picked up " + this.gameObject + "!");
            if (playAudio) audio.PlayOneShot(pickupSound);
            HandleDestroy();
        }
    }

    // Handle collision events
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is coming from the colliding object
        if (collision.gameObject == this.collidingObject)
        {
            if (playAudio) audio.PlayOneShot(pickupSound);
            HandleDestroy();
        }
    }

    private void HandleDestroy ()
    {
        if (this.deleteOnCollide)
        {
            // Disable the gameObject's Mesh Renderer and collider so we can't see or interact with it anymore until it is completely deleted from the scene
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<Collider>().enabled = false;

            // Destroy any light components
            GameObject.Destroy(gameObject.GetComponentInChildren<Light>());
            GameObject.Destroy(gameObject.GetComponent<Light>());

            // Set length of delay for the object to be destroyed; we have to do this so the audio plays correctly
            GameObject.Destroy(gameObject, deleteDelay);
        }
    }

}
