using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ShedManager : MonoBehaviour
{
    private int sheepCollected = 0;
    private int sheepToCollect = 0;
    private GameObject[] activeSheeps;

    [SerializeField]
    TMP_Text text_ShowSheepCollected;

    private void Awake() 
    {
        
    }

    private void Start() 
    {
        // Generate random number of sheep < sheeptocollect in area (for levels < 1)
        sheepToCollect = GameManager2._instance.CurrentLevel() + 1;    
        Debug.Log("Sheep to collect: "+sheepToCollect);
        sheepCollected = 0;
        UpdateText();  
    }

    private void Update() 
    {
        activeSheeps = GameObject.FindGameObjectsWithTag("Sheep");
        if (activeSheeps.Length == 0 && !isFull())
            GameManager2._instance.LoseLevel();
    }

    // Game Manager manages the shed: when a sheep enters, it
    // keeps track of them, destroying objects entering
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.CompareTag("Sheep"))    
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
                GameManager2._instance.WinLevel();
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
    
}
