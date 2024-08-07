using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiniGameManager : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private CheckCode decipherMiniGameCafe;
    [SerializeField] private CheckCode decipherMiniGameElson;
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
        if (state == GameState.decipherMiniGameCafe)
            decipherMiniGameCafe.PlayMiniGame();
        if (state == GameState.decipherMiniGameElson)
            decipherMiniGameElson.PlayMiniGame();
    }
}
