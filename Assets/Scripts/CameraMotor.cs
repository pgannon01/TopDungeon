using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMotor : MonoBehaviour
{
    public Transform lookAt; // Just going to basically be the player, but can use it to put focus on something that isn't the player
    public float boundX = 0.3f; // How far away can the player go in x direction before the camera starts following him? 
    public float boundY = 0.15f;

    private void LateUpdate() // Have to do a LateUpdate for everything camera wise
    {
        // Reason for LateUpdate is because LateUpdate is called after Update and FixedUpdate
        // If we move our player in FixedUpdate, we have to move our camera AFTER he moves. So we can't call it before he moves, we have to call it after
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x; // Checks if we're out of the bounds on the X axis
        if(deltaX > boundX || deltaX < -boundX) // Checks if the player is outside of the bounds either to the right or left
        {
            if(transform.position.x < lookAt.position.x) // If the player is on the right and the camera's focused to the left
            {
                delta.x = deltaX - boundX;
            }
            else 
            {
                delta.x = deltaX + boundX;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y; // Checks if we're out of the bounds on the Y axis
        if(deltaY > boundY || deltaY < -boundY)
        {
            if(transform.position.y < lookAt.position.y) // If the player is on the right and the camera's focused to the left
            {
                delta.y = deltaY - boundY;
            }
            else 
            {
                delta.y = deltaY + boundY;
            }
        }   

        transform.position += new Vector3(delta.x, delta.y, 0);
    }

}
