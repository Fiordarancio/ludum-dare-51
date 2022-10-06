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

    // [SerializeField]
    // LevelManager levelManager;

    [Header("Sounds for lose and win")]
    [SerializeField]
    AudioSource audioSource;
    [SerializeField]
    AudioClip winClip, loseClip;


    private void Start() 
    {
        // Deduce number of sheep to collect from current level
        sheepToCollect = LevelManager._instance.CurrentLevel();
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
                // Delay if there is a sheep mutating but the player can't see it
                if (GameObject.FindGameObjectsWithTag("Sheep2Wolf").Length == 0)
                {
                    isGameRunning = false;
                    audioSource.PlayOneShot(loseClip);
                    UpdateWinText(true, "You lost :(");
                    LevelManager._instance.LoseLevel();
                }
            }
        }
    }

    // Game Manager manages the shed: when a sheep enters, it
    // keeps track of them, destroying objects entering
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (isGameRunning)
        {
            if (other.gameObject.CompareTag("Sheep") || other.gameObject.CompareTag("Sheep2Wolf"))
            {
                // Count it
                sheepCollected++;
                // Destroy it
                Destroy(other.gameObject);
                // Update score
                UpdateText();
                // If we reached the goal, we win
                if (isFull())
                {
                    // Show menu and load to next scene
                    isGameRunning = false;
                    audioSource.PlayOneShot(winClip);
                    UpdateWinText(true, "You won :D");
                    LevelManager._instance.WinLevel();
                }
            }
        }
        if (other.gameObject.CompareTag("Sheep") && isGameRunning)    
        {
            
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
