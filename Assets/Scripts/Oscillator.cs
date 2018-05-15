using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Oscillator : MonoBehaviour {
    [Header("Oscillation Behaviors")]
    [Tooltip("The period of the oscillation. Increasing this number makes the oscillation move slower. Defaults to 2f.")]
    [SerializeField] float period = 2f; // 0 for not moved, 1 fully moved.

    [Range(0, 1)] [SerializeField] float movementFactor; // 0 for not moved, 1 fully moved.
    

    Vector3 startingPos;

    [Tooltip("Where the oscillation should end. Defaults to 10f.")]
    [SerializeField] float endPos;

    [Tooltip("Move in X direction.")]
    [SerializeField] bool moveX;

    [Tooltip("Move in Y direction.")]
    [SerializeField] bool moveY;

    [Tooltip("Move in Z direction.")]
    [SerializeField] bool moveZ;

    // Use this for initialization
    void Start () {
        this.startingPos = gameObject.transform.position; // Get starting position
        this.endPos = 10f;
	}
	
	// Update is called once per frame
	void Update () {
        // Make sure we don't divide by zero
        if (period <= Mathf.Epsilon) { return; }
        // Move the object in a sine oscillation
        float cycles = Time.time / period; // Grows continually from 0, scales the cycle of oscillation when updating

        const float tau = Mathf.PI * 2; // tau is equivalent to 2(Pi)
        float rawSinWave = Mathf.Sin(cycles * tau); // scale it to the cycle modifier

        // Because we halved the amplitude of the sine wave (-1, 1) to (-0.5 to 0.5), then we must shift it up by 0.5
        movementFactor = rawSinWave / 2f + 0.5f;

        // Set the transform by getting the current position and moving it up
        if (this.moveX) gameObject.transform.position = new Vector3((startingPos.x + (movementFactor * endPos)), transform.position.y, transform.position.z);
        if (this.moveY) gameObject.transform.position = new Vector3(transform.position.x, (startingPos.y + (movementFactor * endPos)), transform.position.z);
        if (this.moveZ) gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, (startingPos.z + (movementFactor * endPos)));
    }
}
