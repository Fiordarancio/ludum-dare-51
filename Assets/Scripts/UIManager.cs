using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Listens for events on canvas or global buttons
public class UIManager : MonoBehaviour
{
    [SerializeField]
    GameObject infoCanvas;

    bool showInfo = false;

    // LevelManager levelManager;

    private void Start() 
    {
        showInfo = false;    
    }

    // Update is called once per frame
    void Update()
    {
        // Reload scene by pressing R
        if (Input.GetButtonDown("Reload"))    
            LevelManager._instance.ReloadLevel();

        // Exit game by pressing ESC
        if (Input.GetButtonDown("Exit"))
            LevelManager._instance.ExitGame();

        if (Input.GetButtonDown("Info"))
            ToggleInfo();
    }

    private void ToggleInfo()
    {
        // Toggle canvas activve
        showInfo = !showInfo;
        infoCanvas.SetActive(showInfo);
        // TODO: pause game
    }
}
