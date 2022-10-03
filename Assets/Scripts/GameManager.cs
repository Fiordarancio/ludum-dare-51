using UnityEngine;
using UnityEngine.SceneManagement;
#if UNITY_EDITOR // this directive is set by Unity when we compile and run code in the editor!
using UnityEditor;
#endif

// Single instance to be preserved across sessions
public class GameManager : MonoBehaviour
{
    // Create a static variable to collect the first instance created of this class 
    // and get it accessible everytime and everywhere. In this way, any instance of
    // GameManager will have access to the same instance.
    public static GameManager _instance;

    // This is the value we really want to be persistent
    int currentLevel;

    // Awake, unlike Start, is called at the very beginning of the scene, 
    // when the object in the hierarchy is first created
    private void Awake() {
        if (!_instance)
        {
            _instance = this;               // Initialize by saving the instance the first time 
                                            // => SINGLETON PATTERN
            DontDestroyOnLoad(gameObject);   
            
            // Begin counting levels
            currentLevel = 0;
        }
        else
            Destroy(gameObject); // Destroy any other object

    }

    public int CurrentLevel() 
    {
        return _instance.currentLevel;
    }
    
    public void WinLevel()
    {
        // Show canvas for win level
        Debug.Log("You won! current level " + currentLevel);

        currentLevel++;
        ReloadLevel();
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

    public void ExitGame ()
    {
        #if UNITY_EDITOR // checks at compilation time if the code is running on the editor
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
