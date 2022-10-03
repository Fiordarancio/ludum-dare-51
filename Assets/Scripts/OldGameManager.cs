using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

// Single instance to be preserved across sessions
public class OldGameManager : MonoBehaviour
{
    // Create a static variable to collect the first instance created of this class 
    // and get it accessible everytime and everywhere. In this way, any instance of
    // MainManager will have access to the same instance.
    public static OldGameManager _instance;

    RainManager rainManager;
    int currentLevel;

    // Awake, unlike Start, is called at the very beginning of the scene, when the 
    // object in the hierarchy is first created
    private void Awake() {
        if (!_instance)
        {
            _instance = this;    // Initialize by saving the instance the first time 
                                // => SINGLETON PATTERN
            DontDestroyOnLoad(gameObject);  // On loading scenes, don't destroy

            // Begin counting levels
            Debug.Log("First awake");
            currentLevel = -1;
            // Check and set the rain manager
            rainManager = GetComponent<RainManager>();
            if (rainManager == null)
                rainManager = gameObject.AddComponent<RainManager>();
        }
        else
            Destroy(gameObject); // Destroy any other object

        Debug.Log("Another awake?");
        // Select a color if there is one saved
        currentLevel++;
    }

    private void Update() 
    {
        // Reload scene by pressing R
        if (Input.GetButtonDown("ReloadScene"))    
            ReloadLevel();
    }

    public int CurrentLevel() 
    {
        return _instance.currentLevel;
    }
    
    public void WinLevel()
    {
        // Show canvas for win level
        Debug.Log("You won! current level " + currentLevel);
        // Stop rain and everything
        rainManager.StopAllCoroutines();

        // Load next scene
        if (currentLevel == 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        else
            ReloadLevel();

        currentLevel++;

    }

    public void LoseLevel()
    {
        // The shed isn't full and no other sheep survived
        // Show canvas and reload level
        Debug.Log("You lose.");
        ReloadLevel();
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
