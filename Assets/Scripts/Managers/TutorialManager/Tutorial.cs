using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    public int Order;

    [TextArea(3,10)]
    public string Explanation;

    protected bool isProcessing = false;

    private void Awake()
    {
        if (TutorialManager.GetInstance == null)
        {
            Debug.LogError("TutorialManager is not initialized.");
            return;
        }

        TutorialManager.GetInstance.Tutorials.Add(this);
    }

    public virtual void CheckIfHappening() { }
}
