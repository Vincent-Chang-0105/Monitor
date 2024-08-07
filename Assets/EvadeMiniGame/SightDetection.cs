using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

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

            //if (playerDetected)
            //{
            //    Debug.Log("Day resetting.");
            //    GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.HumanScene);
            //}

            Time.timeScale = 0f;
        }
    }
}
