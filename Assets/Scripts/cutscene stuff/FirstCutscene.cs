using System.Collections;
using UnityEngine;
using UnityEngine.Video;

public class FirstCutscene : MonoBehaviour
{
    public VideoPlayer videoPlayer; // Reference to the VideoPlayer component
    public GameObject videoCanvas;  // Reference to the video canvas GameObject
    public GameObject tutorial;

    private void Start()
    {
        // Ensure the Collider is set as a trigger
        Collider collider = GetComponent<Collider>();
        if (collider != null)
        {
            collider.isTrigger = true;
        }
        else
        {
            Debug.LogError("No Collider found on this GameObject.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to the player
        if (other.CompareTag("Player"))
        {
            videoCanvas.SetActive(true);
            tutorial.SetActive(false);
            // Call a method to start the cutscene
            StartCoroutine(StartCutscene());
        }
    }

    private IEnumerator StartCutscene()
    {

        // Disable player movement
        GameEventsManager.Instance.playerEvents.DisablePlayerMovement();
        Debug.Log("Player movement disabled");

        // Activate the video canvas
        //videoCanvas.SetActive(true);

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

        yield return new WaitForSeconds(84f); // Wait for the next frame

        Debug.Log("Video finished playing");
        videoCanvas.SetActive(false);
        tutorial.SetActive(true);
        // Re-enable player movement after the video finishes
        GameEventsManager.Instance.playerEvents.EnablePlayerMovement();
        GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.HumanScene);
        Debug.Log("Player movement enabled");
    }
}
