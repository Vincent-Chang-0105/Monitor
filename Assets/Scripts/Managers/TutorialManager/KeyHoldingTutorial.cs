using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyHoldingTutorial : Tutorial
{
    public KeyCode keyToHold = KeyCode.LeftAlt; // The key to hold
    public float holdDuration = 2f; // Duration to hold the key to complete the tutorial

    private bool isHoldingKey = false; // Flag to check if the key is being held
    private float holdStartTime; // Time when the key was first pressed

    public override void CheckIfHappening()
    { 
        // Check if the key is currently being held down
        if (Input.GetKey(keyToHold))
        {
            if (!isHoldingKey)
            {
                // If the key was not previously held, start tracking the time
                isHoldingKey = true;
                holdStartTime = Time.time;
            }
            else
            {
                // If the key has been held long enough, complete the tutorial
                if (Time.time - holdStartTime >= holdDuration)
                {
                    if (!isProcessing)
                    {
                        isProcessing = true;
                        StartCoroutine(TutorialManager.GetInstance.TutorialSuccess());
                    }
                }
            }
        }
        else
        {
            // Reset if the key is released
            isHoldingKey = false;
        }
    }
}
