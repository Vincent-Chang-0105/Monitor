using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class SightDetection : MonoBehaviour
{
    public TMP_Text alertText; // Reference to the TMP text element to display the alert
    private bool playerDetected = false; // Flag to track if player is detected
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public GameObject videoCanvas;  // Reference to the video canvas GameObject
    public GameObject tutorial;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !playerDetected)
        {
            playerDetected = true; // Set the flag to true to prevent multiple detections

            Debug.Log("Player entered sight detection!");

            // Display alert text
            alertText.gameObject.SetActive(true);
            alertText.text = "Player Detected!";

            // Stop the game (you may want to freeze gameplay or prevent further interaction)
            Time.timeScale = 0f; // This stops time-based gameplay elements

            // Example: Trigger an alert or any other action
            StartCoroutine(TriggerAlert());
        }
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
