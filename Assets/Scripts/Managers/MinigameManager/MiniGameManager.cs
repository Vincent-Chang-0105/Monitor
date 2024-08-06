using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject decipherMiniGame;
    [SerializeField] private GameObject evadeMinigame;

    void Start()
    { 
        GameEventsManager.Instance.gameEvents.onStateChange += GameEvents_onStateChange;
    }

    private void OnDestroy()
    {
        GameEventsManager.Instance.gameEvents.onStateChange -= GameEvents_onStateChange;
    }

    private void GameEvents_onStateChange(GameState state)
    {
        decipherMiniGame.SetActive(state == GameState.decipherMiniGame);
    }
}
