using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable
{
    // Logic 
    protected bool collected;
    // cannot use it if it's private, but if it's public everyone could use it, so we use protected, so it's only passed on to its children

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
            OnCollect();
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
