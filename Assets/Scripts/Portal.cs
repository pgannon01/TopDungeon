using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    public string[] sceneNames;

    protected override void OnCollide(Collider2D coll)
    {
        if(coll.name == "Player")
        {
            GameManager.instance.SaveState(); // This will call SaveState on any change of the scene, saving our game on every scene change

            // Teleport the player to the next level (random dungeon)
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];

            // Multiple ways to load a scene in unity, below is one
            SceneManager.LoadScene(sceneName); // Absolutely need to use the using SceneManagement call. 
            // Can do it at the top or can do it inline with this call:
            // UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
        }
    }
}
