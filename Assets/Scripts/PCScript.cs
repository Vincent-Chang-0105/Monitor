using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PCScript : MonoBehaviour
{
    public GameObject UI;

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
        GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.RobotScene);
    }
}
