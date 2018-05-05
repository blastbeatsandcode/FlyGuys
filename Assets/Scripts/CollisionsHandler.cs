using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class CollisionsHandler : MonoBehaviour {
    // Rigidbody rigidbody;

    // object we want to collide with.
    [Header("Collision Settings")]

    [Tooltip("Object we want to collide with. Defaults to Player tag")]
    [SerializeField] GameObject collidingObject;

    [Tooltip("Check if this object should be deleted with colliding object")]
    [SerializeField] bool deleteOnCollide = false;

    [Tooltip("Check if this object is a pickup item and toggles trigger on collider component")]
    [SerializeField] bool isPickup = false;

    // Use this for initialization
    void Start () {
        // Get rigidbody of this component
        //this.rigidbody = gameObject.GetComponent<Rigidbody>();

        // Get instance of player
        this.collidingObject = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        // If we have checked isPickup, force collider to be a trigger
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
        }
        HandleDelete();
    }

    // Handle collision events
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is coming from the colliding object
        if (collision.gameObject == this.collidingObject)
        {
            HandleDelete();
        }
    }

    private void HandleDelete ()
    {
        if (this.deleteOnCollide)
        {
            GameObject.Destroy(gameObject);
        }
    }

}
