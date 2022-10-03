using System.Collections;
using UnityEngine;
using TMPro;

// End level manager
public class ShedManager : MonoBehaviour
{
    private int sheepCollected = 0;
    private int sheepToCollect = 0;
    private GameObject[] activeSheeps;

    bool isGameRunning = true;

    [SerializeField]
    TMP_Text text_ShowSheepCollected;
    [SerializeField]
    TMP_Text text_WinLose;

    [SerializeField]
    LevelManager levelManager;


    private void Start() 
    {
        // Deduce number of sheep to collect from current level
        sheepToCollect = levelManager.CurrentLevel();
        sheepCollected = 0;
        UpdateText();  
        UpdateWinText(false);
        
        isGameRunning = true;
    }

    private void Update() 
    {
        if (isGameRunning)
        {
            activeSheeps = GameObject.FindGameObjectsWithTag("Sheep");
            if (activeSheeps.Length == 0 && !isFull())
            {
                isGameRunning = false;
                UpdateWinText(false, "You lost :(");
                levelManager.LoseLevel();
            }
        }
    }

    // Game Manager manages the shed: when a sheep enters, it
    // keeps track of them, destroying objects entering
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Sheep") && isGameRunning)    
        {
            // Count it
            sheepCollected++;
            UpdateText();
            // Destroy it
            Destroy(other.gameObject);
            // If we reached the goal, we win
            if (isFull())
            {
                // Show menu and load to next scene
                isGameRunning = false;
                UpdateWinText(true, "You won :D");
                levelManager.WinLevel();
            }
        }
    }
    
    private void UpdateText()
    {
        text_ShowSheepCollected.text = sheepCollected + "/" + sheepToCollect;
    }
    private void UpdateWinText(bool show, string msg = "")
    {
        text_WinLose.text = msg;
        text_WinLose.gameObject.SetActive(show);
    }

    private bool isFull ()
    {
        return (sheepCollected >= sheepToCollect);
    }

}
