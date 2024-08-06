using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class TutorialManager : MonoBehaviour
{
    private static TutorialManager Instance;
    public static TutorialManager GetInstance
    {
        get
        {
            if (Instance == null)
                Instance = FindObjectOfType<TutorialManager>();

            if (Instance == null)
                Debug.LogError("TutorialManager is not Found.");

            return Instance;
        }
    }

    public List<Tutorial> Tutorials = new List<Tutorial>();

    public TextMeshProUGUI expText;

    public Animator animator;
    public Tutorial currentTutorial { get; private set; }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (currentTutorial)
            currentTutorial.CheckIfHappening();
    }
    
    public void CompletedTutorial()
    {
        SetNextTutorial(currentTutorial.Order + 1);
    }
    public void SetNextTutorial(int currentOrder)
    {
        currentTutorial = GetTutorialByOrder(currentOrder);

        if(!currentTutorial) 
        {
            CompletedAllTutorials();
            return;
        }

        expText.text = currentTutorial.Explanation;
        animator.Play("ObjectiveDisplayAnim");
    }

    public void CompletedAllTutorials()
    {
        expText.text = "You have completed all the tutorials, yipee";
    }
    public Tutorial GetTutorialByOrder(int order)
    {
        for (int i = 0; i < Tutorials.Count; i++)
        {
            if (Tutorials[i].Order == order)
                return Tutorials[i];
        }

        return null;
    }

    public IEnumerator TutorialSuccess()
    {
        expText.text += " - Complete";
        animator.Play("RemoveDisplayAnim");
        // Use WaitForSeconds instead of WaitForSecondsRealtime
        yield return new WaitForSeconds(1.5f);
        CompletedTutorial();
    }
}
