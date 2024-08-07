using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using System.Collections;

public class Countdown : MonoBehaviour
{
    public TMP_Text timerText; // Reference to the TMP text element for displaying the timer
    public TMP_Text winText; // Reference to the TMP text element for displaying the win message
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public GameObject videoCanvas;  // Reference to the video canvas GameObject
    public GameObject tutorial;

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
        //Time.timeScale = 0f; // This stops time-based gameplay elements

        // Show win message
        winText.text = "Congratulations! You have escaped!"; // Set win message text
        winText.gameObject.SetActive(true); // Make win message visible

        StartCoroutine(TriggerAlert());

        // Example: Trigger win screen, reset level, etc.
        // ShowWinScreen();
    }

    private IEnumerator TriggerAlert()
    {
        // Implement your alert logic here
        Debug.Log("Alert triggered!");

        yield return new WaitForSecondsRealtime(2.0f);
        Time.timeScale = 1f;
        StartCoroutine(StartCutscene());
        //GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.HumanScene);
        // Example: Play an alarm sound, change AI behavior, etc.
    }

    private IEnumerator StartCutscene()
    {

        // Disable player movement
        GameEventsManager.Instance.playerEvents.DisablePlayerMovement();
        Debug.Log("Player movement disabled");

        // Activate the video canvas
        videoCanvas.SetActive(true);

        Debug.Log("Video canvas activated");

        // Ensure the VideoPlayer component is assigned
        if (videoPlayer != null)
        {
            // Reset the video to the start
            videoPlayer.time = 0;
            Debug.Log("Video time reset");

            // Play the video
            videoPlayer.Play();
            Debug.Log("Video started playing");


        }
        else
        {
            Debug.LogError("No VideoPlayer component assigned.");
        }

        yield return new WaitForSeconds(80f); // Wait for the next frame

        Debug.Log("Video finished playing");
        videoCanvas.SetActive(false);
        //tutorial.SetActive(true);
        // Re-enable player movement after the video finishes
        SceneManager.LoadScene("MainMenuScene");
        Debug.Log("Player movement enabled");
    }
}
