using UnityEngine;
using TMPro;

public class SightDetection : MonoBehaviour
{
    public TMP_Text alertText; // Reference to the TMP text element to display the alert
    private bool playerDetected = false; // Flag to track if player is detected

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
            TriggerAlert();
        }
    }

    private void TriggerAlert()
    {
        // Implement your alert logic here
        Debug.Log("Alert triggered!");

        // Example: Play an alarm sound, change AI behavior, etc.
    }
}
