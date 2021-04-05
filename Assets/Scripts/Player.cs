using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private BoxCollider2D boxCollider;

    private Vector3 moveDelta; 
    // moveDelta means in between this frame that we're rendering now and the next one, what is gonna be the difference between our current position...
    // and where we're going to be? By the end of this frame we're going to be adding our players current position with the move delta...
    // and we'll end up where we want to be
    private RaycastHit2D hit; // For collision

    private void Start() // Only ran once 
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate() // Movement update loop
    {
        // Could use just Update() but since we're using physics and manual collision detection we have to do FixedUpdate()
        // This will follow the same frame as the physics, so it's important to use FixedUpdate()
        // If you're looking for inputs you probably don't want FixedUpdate because it can sometime skip one 

        float x = Input.GetAxisRaw("Horizontal"); // Look for inputs on the keyboard, this is for our delta in x
        // GetAxisRaw will return -1, 1, or 0. -1 if holding A key, 1 if holding D key and 0 if holding neither
        // Inputs are automatically set by unity in the Input Manager
        float y = Input.GetAxisRaw("Vertical");

        // Reset moveDelta
        // This is so on the newer frame we go back to zero, in case we don't have any movement input anymore
        moveDelta = new Vector3(x,y,0);

        // Swap sprite direction, wether you're going right or left
        if(moveDelta.x > 0)
            transform.localScale = Vector3.one; // Could also do: new Vector3(1,1,1) but this will save some memory as it's already declared
        else if(moveDelta.x < 0)
            transform.localScale = new Vector3(-1,1,1); // Only changing scale from 1 to -1 depending on if we're going left or right
            // Also using an else if statement because we only want it to look for whether we're moving right or left
            // If we're not moving, and it's at 0, we don't want to be flipping back and forth every time

        // Make sure we can move in this direction, by casting a box there first
        // If the box returns null we're free to move
        hit = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(0,moveDelta.y), Mathf.Abs(moveDelta.y * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
            // Make it move
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0); // To make sure the movement speed is the same for every device, regardless of framerate
        }

        hit = Physics2D.BoxCast(transform.position,boxCollider.size, 0, new Vector2(moveDelta.x, 0), Mathf.Abs(moveDelta.x * Time.deltaTime), LayerMask.GetMask("Actor", "Blocking"));
        if(hit.collider == null)
        {
            // Make it move
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0); // To make sure the movement speed is the same for every device, regardless of framerate
        }

    }
}
