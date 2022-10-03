using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    float waitForReload = 5f;

    private void Start() 
    {
        // Generate random number of sheep < sheeptocollect in area (for levels < 1)
        sheepToCollect = GameManager._instance.CurrentLevel() + 1;    
        Debug.Log("Sheep to collect: "+sheepToCollect);
        sheepCollected = 0;
        UpdateText();  
        text_WinLose.gameObject.SetActive(false);
        
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
                StartCoroutine(IssueLose());
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
                StartCoroutine(IssueWin());
            }
        }
    }
    
    private void UpdateText()
    {
        text_ShowSheepCollected.text = sheepCollected + "/" + sheepToCollect;
    }

    private bool isFull ()
    {
        return (sheepCollected >= sheepToCollect);
    }
    
    private IEnumerator IssueWin()
    {
        text_WinLose.text = "You won :D";
        text_WinLose.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitForReload);
        GameManager._instance.WinLevel();
    }

    private IEnumerator IssueLose()
    {
        text_WinLose.text = "You lost :(";
        text_WinLose.gameObject.SetActive(true);
        yield return new WaitForSeconds(waitForReload);
        GameManager._instance.LoseLevel();
    }
}
