using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Since we won the game, the GameManager is no more needed
public class DestroyGameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameObject manager = GameObject.Find("Game Manager");
        if (manager != null)
        {
            Debug.Log("Game Manager is done. Bye bye");
            Destroy(manager);
        }
    }
}
