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
        Debug.Log("You won! current level " + currentLevel);

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

    public void ExitGame ()
    {
        #if UNITY_EDITOR // checks at compilation time if the code is running on the editor
            EditorApplication.ExitPlaymode();
        #else
            Application.Quit();
        #endif
    }
}
