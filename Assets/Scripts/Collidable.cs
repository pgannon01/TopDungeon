using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collidable : MonoBehaviour
{
    public ContactFilter2D filter; 
    private BoxCollider2D boxCollider; // private because since it's on the object we don't need to put this on every object
    // Will only assign on start or await call
    private Collider2D[] hits = new Collider2D[10];
    // An array containing data of what we hit, can hold a max of 10 items in the array, but even this is a lot, calculated per frame

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        // This means that whatever object our collidable script is on, it requires a BoxCollider2D component
        // Could also do: [RequireComponent(typeof(BoxCollider2D))]
        // This will do the same thing, only it will add that component to the script
    }

    protected virtual void Update() // virtual means you have the opportunity to change or override them, through inheritance and children
    {
        // Collision work
        boxCollider.OverlapCollider(filter, hits);
        // This will take our boxCollider and look for something above or beneath it, basically look to see if anything is colliding with it
        // Will then put it inside of the hits array
        for (int i = 0; i < hits.Length; i++)
        {
            if(hits[i] == null)
                continue;

            OnCollide(hits[i]);

            // The array is not cleaned up every time, so we do it ourself
            hits[i] = null;
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        Debug.Log(coll.name);
    }

}
