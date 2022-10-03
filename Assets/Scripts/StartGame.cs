using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR 
using UnityEditor;
#endif

public class StartGame : MonoBehaviour
{

    private void Update() 
    {
        if (Input.GetKeyDown(KeyCode.Escape))    
            exitGame();

        if (Input.GetKeyDown(KeyCode.Space))
            startGame();
    }

    public void startGame() 
    {
        Debug.Log("Clicked");
        SceneManager.LoadScene(1);
    }

    public void exitGame()
    {
        #if UNITY_EDITOR 
        EditorApplication.ExitPlaymode();
        #else
        Application.Quit();
        #endif
    }
}
