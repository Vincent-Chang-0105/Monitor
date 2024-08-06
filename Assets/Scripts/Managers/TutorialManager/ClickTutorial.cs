using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickTutorial : Tutorial
{
    private bool isCurrentTutorial = false;

    public GameObject sourceGameObject;



    public override void CheckIfHappening()
    {
        if (isProcessing) return;

        Debug.Log("tite");
        if (sourceGameObject.activeInHierarchy)
        {
            isProcessing = true;
            StartCoroutine(TutorialManager.GetInstance.TutorialSuccess());
        }
    }

}
