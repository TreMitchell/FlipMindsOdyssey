using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; // Singleton instance

    public float timeElapsed { get; private set; } // Total time elapsed
    private bool isTimerRunning = false; // Flag to check if the timer is running

    private void Awake()
    {
        // Ensure only one instance of GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene changes
        }
        else
        {
            Destroy(gameObject); // Destroy duplicate GameManager objects
        }
    }

    private void Update()
    {
        // Update the timer if it's running
        if (isTimerRunning)
        {
            timeElapsed += Time.deltaTime;
        }
    }

    // Start the timer
    public void StartTimer()
    {
        isTimerRunning = true;
    }

    // Stop the timer
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // Reset the timer
    public void ResetTimer()
    {
        timeElapsed = 0f;
    }
}
