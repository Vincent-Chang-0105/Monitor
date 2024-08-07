using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Cameras")]
    [SerializeField] Camera humanCamera;
    [SerializeField] Camera robotCamera;
    [SerializeField] GameObject evadeCamera;
    [SerializeField] GameObject decipher1;
    [SerializeField] GameObject decipher2;

    [HideInInspector] public GameState state;
    private void Awake()
    {
        GameEventsManager.Instance.gameEvents.onUpdateGameState += UpdateGameState;
    }

    private void OnDestroy()
    {
        GameEventsManager.Instance.gameEvents.onUpdateGameState -= UpdateGameState;
    }

    private void Start()
    {
        UpdateGameState(GameState.HumanScene);
    }

    public void UpdateGameState(GameState newState)
    {
        state = newState;

        switch (newState)
        {
            case GameState.HumanScene:
                HandleSwitchToHuman();
                Debug.Log("changed to HumanScene");
                break;
            case GameState.RobotScene:
                HandleSwitchToRobot();
                Debug.Log("changed to RobotScene");
                break;
            case GameState.decipherMiniGameCafe:
                HandleDecipher1();
                break;
            case GameState.decipherMiniGameElson:
                HandleDecipher1();
                break;
            case GameState.evadeMiniGame:
                HandleEvadeMinigame();
                break;
            default:
                break;
        }
        GameEventsManager.Instance.gameEvents.StateChange(newState);
    }

    private void HandleSwitchToHuman()
    {
        humanCamera.gameObject.SetActive(true);
        robotCamera.gameObject.SetActive(false);
        evadeCamera.gameObject.SetActive(false);
    }

    private void HandleSwitchToRobot()
    {
        robotCamera.gameObject.SetActive(true);
        humanCamera.gameObject.SetActive(false);
        evadeCamera.gameObject.SetActive(false);
    }

    private void HandleEvadeMinigame()
    {
        robotCamera.gameObject.SetActive(false);
        humanCamera.gameObject.SetActive(false);

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        evadeCamera.gameObject.SetActive(true);
    }

    private void HandleDecipher1()
    {
        decipher1.SetActive(true);
    }
    private void HandleDecipher2()
    {
        decipher1.SetActive(false);
    }
}

public enum GameState
{
    HumanScene,
    RobotScene,
    decipherMiniGameCafe,
    decipherMiniGameElson,
    evadeMiniGame
}