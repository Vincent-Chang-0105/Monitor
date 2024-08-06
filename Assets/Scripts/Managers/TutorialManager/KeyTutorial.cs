using System.Collections;
using System.Collections.Generic;
using Unity.PlasticSCM.Editor.WebApi;
using UnityEngine;

public class KeyTutorial : Tutorial 
{
    public List<string> Keys = new List<string>();

    public override void CheckIfHappening()
    {
        if (isProcessing) return;
        for (int i = 0; i < Keys.Count; i++)
        {
            if (Input.inputString.Contains(Keys[i]))
            {
                Keys.RemoveAt(i);
                break;
            }
        }

        if (Keys.Count == 0)
        {
            isProcessing = true;
            StartCoroutine(TutorialManager.GetInstance.TutorialSuccess());
        }

    }
}
