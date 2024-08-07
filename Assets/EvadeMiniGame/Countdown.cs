using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the TMP text element for displaying the timer
    public TMP_Text winText; // Reference to the TMP text element for displaying the win message

    private float totalTime = 15f; // Total time for the timer in seconds
    private float timeLeft; // Time left on the timer
    private bool timerRunning = true; // Flag to control if the timer is running

    void Start()
    {
        timeLeft = totalTime;
        winText.gameObject.SetActive(false); // Ensure win message is hidden initially
    }

    void Update()
    {
        if (timerRunning)
        {
            timeLeft -= Time.deltaTime; // Count down the timer
            UpdateTimerDisplay(); // Update the timer display

            if (timeLeft <= 0)
            {
                timeLeft = 0;
                timerRunning = false; // Stop the timer
                Debug.Log("Timer has finished!");

                // Call the method to handle win condition
                HandleWin();
            }
        }
    }

    void UpdateTimerDisplay()
    {
        int seconds = Mathf.CeilToInt(timeLeft); // Round up to the nearest second
        timerText.text = seconds.ToString(); // Update the TMP text with the current time left
    }

    void HandleWin()
    {
        // Stop the game (you may want to freeze gameplay or prevent further interaction)
        Time.timeScale = 0f; // This stops time-based gameplay elements

        // Show win message
        winText.text = "Congratulations! You have escaped!"; // Set win message text
        winText.gameObject.SetActive(true); // Make win message visible

        // Example: Trigger win screen, reset level, etc.
        // ShowWinScreen();
    }
}
