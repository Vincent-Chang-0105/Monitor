using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeciperUnload : MonoBehaviour
{
    public GameObject decipher1;
    public GameObject decipher2;
    public CheckCode d1;
    public CheckCode d2;

    private bool isDecipher1Handled = false;
    private bool isDecipher2Handled = false;

    private void Update()
    {
        if (d1.finished && !isDecipher1Handled)
        {
            decipher1.SetActive(false);
            isDecipher1Handled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.RobotScene);
        }

        if (d2.finished && !isDecipher2Handled)
        {
            decipher2.SetActive(false);
            isDecipher2Handled = true;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.RobotScene);
        }
    }
}
