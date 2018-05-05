using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Rocket : MonoBehaviour {
    // Hold rigid body of the rocket ship
    Rigidbody rigidbody;
    AudioSource audio;

    [Header("Rocket Settings")]

    [Tooltip("Speed of left or right rotation")]
    [SerializeField] float rotationSpeed = 100f;
    [Tooltip("Thrust speed of rocket")]
    [SerializeField] float thrustSpeed = 100f;

    // Use this for initialization
    void Start () {
        // Grab rigidbody component from gameobject
        this.rigidbody = GetComponent<Rigidbody>();

        // Grab audio source for rocket
        this.audio = GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
        Thrust();
        Rotate();
	}

    private void OnCollisionEnter(Collision collision)
    {
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                print("okay");
                break;
            case "Pickup":
                print("encountered pickup; might not ever occur because pickups are usually triggers");
                break;
            default: // Everything else makes us blow up
                print("die");
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
       // if (other.gameObject.CompareTag("Pick Up"))
       // {
        //    GameObject.Destroy(other.gameObject);
       // }
    }

    private void Thrust()
    {
        // Check to see if spacebar is being pressed
        if (Input.GetKey(KeyCode.Space))
        {
            // Relative force will move the object in the direction specified relative to where it's currently positioned
            float thrustThisFrame = thrustSpeed * Time.deltaTime;
            rigidbody.AddRelativeForce(Vector3.up * thrustThisFrame);

            // Play audio if it is not already playing
            if (!audio.isPlaying)
            {
                audio.Play();
            }
        }
        else
        {
            audio.Stop();
        }
    }

    private void Rotate()
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
