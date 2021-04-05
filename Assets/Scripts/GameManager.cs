using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    // static is something you can access from anywhere in your code
    // So in another file you can access GameManager.instance
    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(gameObject);
            return;
            // This code is to make sure that, when you switch levels, you don't get two instances of GameManager running at once
            // This isn't the best way to go about it, but it is one way to do it
        }

        // PlayerPrefs.DeleteAll(); // Way to delete your saves

        // Can only have one GameManager so we have to make sure that the instance = this
        instance = this;
        // So in Unity we need an empty GameObject, that we name GameManager and put this component on
        // That will be our ONE and ONLY game manager, which we can access elsewhere
        SceneManager.sceneLoaded += LoadState; // Will run the provided function on the loading of any scene
        DontDestroyOnLoad(gameObject); // As soon as you start the game and change scenes, the game manager will persist
        // This way GameManager will persist across every level without needing to copy it over to each individual scene
    }

    // Resources for the game
    public List<Sprite> playerSprites;
    public List<Sprite> weaponSprites;
    public List<int> weaponPrices;
    public List<int> xpTable;

    // References to things like player script, weapons script, etc.
    public Player player;
    // public Weapon weapon;

    // Something to keep track of gold and experience
    public int gold;
    public int experience;

    // Save State

    public void SaveState() // in order for this to work with sceneLoaded we need Scene and LoadSceneMode variables
    {
        // Function to save your game

        // SceneManager.sceneLoaded -= SaveState; // This will make it so that on the initial loading it will call the function, as above...
        // But once it's called this function will remove itself from the sceneLoaded call, so it won't ever be called again on sceneLoaded

        string s = ""; // s for saving

        s += "0" + "|"; // For preferred skin
        s += gold.ToString() + "|"; // starting gold
        s += experience.ToString() + "|"; // starting exp
        s += "0"; // starting weapon level

        PlayerPrefs.SetString("SaveState", s);
    }

    public void LoadState(Scene s, LoadSceneMode mode)
    {
        // Load your saved game(s)
        if(!PlayerPrefs.HasKey("SaveState"))
            return;

        string[] data = PlayerPrefs.GetString("SaveState").Split('|'); //NEED SINGLE QUOTES NOT DOUBLE HERE
        
        // Change Player Skin
        gold = int.Parse(data[1]);
        experience = int.Parse(data[2]);
        // Change the weapon level

    }

}
