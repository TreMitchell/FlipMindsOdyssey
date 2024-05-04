using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public static GameController instance; // Singleton instance
    public Text timerText; // Reference to the Text component displaying the timer

    private float timer = 0f; // Timer value

    void Awake()
    {
        // Ensure only one instance of GameController exists
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        // Update the timer value
        timer += Time.deltaTime;

        // Update the timer text
        UpdateTimerText();
    }

    // Example method to start the game
    public void StartGame()
    {
        // Start the timer when the game begins
        timer = 0f;
    }

    // Example method to end the game
    public void EndGame()
    {
        // Stop the timer when the game ends
        UpdateTimerText(); // Update the timer text one last time
    }

    // Example method to update the timer text
    void UpdateTimerText()
    {
        // Format the timer value as minutes and seconds
        int minutes = Mathf.FloorToInt(timer / 60f);
        int seconds = Mathf.FloorToInt(timer % 60f);
        string timerString = string.Format("{0:00}:{1:00}", minutes, seconds);

        // Update the Text component with the formatted timer string
        if (timerText != null)
        {
            timerText.text = "Time: " + timerString;
        }
    }
}
