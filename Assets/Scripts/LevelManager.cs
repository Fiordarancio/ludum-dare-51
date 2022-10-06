using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
#if UNITY_EDITOR // this directive is set by Unity when we compile and run code in the editor!
using UnityEditor;
#endif

public class LevelManager : MonoBehaviour
{
    // This is the value we really want to be persistent
    private int currentLevel = 0;

    [SerializeField]
    float loadDelay = 2f;
    [SerializeField]
    AudioClip gameLoopClip;
    AudioSource audioSource;

    public static LevelManager _instance;

    private void Awake() 
    {
        Debug.Log("Awake issued for LevelManager");

        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);  // Thanks to this, Awake and Start will be called jut once
                                            // on this object, while any other instance shall be dismissed

            Debug.Log("Created Game Manager!");
        }
        else
        {
            Debug.Log("I'm a new Game Manager instance, but not the very first, so I'll die. (Better luck next time)");
            Destroy(gameObject);
        }

        // This part of the block is reached only once
        
        // Set the beginning level
        _instance.currentLevel = SceneManager.GetActiveScene().buildIndex;
        // Make the audio play
        audioSource = MakeAudioSource();
        audioSource.Play();
    }

    public int CurrentLevel() 
    {
        return _instance.currentLevel;
    }
    
    public void WinLevel()
    {
        // Show canvas for win level
        Debug.Log("You won!");

        _instance.StartCoroutine(_instance.NextLevel(_instance.currentLevel+1));
    }

    public void LoseLevel()
    {
        // The shed isn't full and no other sheep survived
        // Show canvas and reload level
        Debug.Log("You lose.");

        _instance.StartCoroutine(_instance.NextLevel(currentLevel));
    }

    private IEnumerator NextLevel(int index)
    {
        yield return new WaitForSeconds(_instance.loadDelay);
        
        // Save which level we are loading
        _instance.currentLevel = index;
        Debug.Log("Loading level "+ _instance.currentLevel);
        
        SceneManager.LoadScene(index);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(_instance.currentLevel);
    }

    public void ExitGame ()
    {
        #if UNITY_EDITOR // checks at compilation time if the code is running on the editor
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }

    // Audio managing stuff
    private AudioSource MakeAudioSource()
    {
        if (audioSource == null)
        {
            // Create audiosource for the first time
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.volume = 0.7f;
        audioSource.loop = true;
        audioSource.playOnAwake = false;

        audioSource.clip = gameLoopClip;
        
        return audioSource;
    }
}

/**
    // This is the value we really want to be persistent
    private int currentLevel = 0;

    [SerializeField]
    float loadDelay = 2f;

    private void Awake() {
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    public int CurrentLevel() 
    {
        return currentLevel;
    }
    
    public void WinLevel()
    {
        // Show canvas for win level
        Debug.Log("You won!");

        StartCoroutine(NextLevel(currentLevel+1));
    }

    public void LoseLevel()
    {
        // The shed isn't full and no other sheep survived
        // Show canvas and reload level
        Debug.Log("You lose.");

        StartCoroutine(NextLevel(currentLevel));
    }

    private IEnumerator NextLevel(int index)
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(index);
    }

    public void ReloadLevel()
    {
        SceneManager.LoadScene(currentLevel);
    }

    public void ExitGame ()
    {
        #if UNITY_EDITOR // checks at compilation time if the code is running on the editor
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }   

**/
