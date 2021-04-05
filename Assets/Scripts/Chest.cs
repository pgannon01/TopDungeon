using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest; // So we can swap the full chest sprite to the empty chest one once we've collected our gold
    public int goldAmount = 10; // Tells us how much gold we earn from the chest

    protected override void OnCollect()
    {
        if(!collected)
        {
            collected = true;
            GetComponent<SpriteRenderer>().sprite = emptyChest;
            Debug.Log("Grant " + goldAmount + " gold!");
        }
    }
}
