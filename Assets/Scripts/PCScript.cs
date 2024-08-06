using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PCScript : MonoBehaviour
{
    public GameObject UI;
    public GameObject dialogueBox; // Assign the dialogue box GameObject in the Inspector

    private bool hasDialogueShown = false; // Track if the dialogue has been shown

    void Start()
    {
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameEventsManager.Instance.playerEvents.EnablePlayerMovement();
        }
    }

    private void OnMouseDown()
    {
        UI.SetActive(true);
        GameEventsManager.Instance.playerEvents.DisablePlayerMovement();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ChangeToRobot()
    {
        UI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameEventsManager.Instance.playerEvents.EnablePlayerMovement();

        if (!hasDialogueShown)
        {
            // Show the dialogue box
            dialogueBox.SetActive(true);
            hasDialogueShown = true; // Mark the dialogue as shown
        }
        else
        {
            // Optionally, you can hide the dialogue box if it's already shown
            dialogueBox.SetActive(false);
        }

        GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.RobotScene);
    }
}
