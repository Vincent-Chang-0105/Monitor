using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClickTutorial : Tutorial
{
    public Button uiButton;
    private bool isCurrentTutorial = false;
    // Start is called before the first frame update
    void Start()
    {
        if (uiButton != null)
        {
            // Add a listener to the button's onClick event
            uiButton.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnDestroy()
    {
        if (uiButton != null)
        {
            // Remove the listener when this object is destroyed
            uiButton.onClick.RemoveListener(OnButtonClick);
        }
    }

    public override void CheckIfHappening()
    {
        isCurrentTutorial = true;
    }
    private void OnButtonClick()
    {
        if (!isCurrentTutorial) return;

        StartCoroutine(TutorialManager.GetInstance.TutorialSuccess());
    }
}
