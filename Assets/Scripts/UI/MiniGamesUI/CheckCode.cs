using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CheckCode : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private TextMeshProUGUI notifierText;
    [SerializeField] private TextMeshProUGUI decipherText;
    [SerializeField] private TMP_InputField inputTextField;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] public ICodeChecker codeChecker;

    private float currenttime;
    private float startingtime = 10.5f;

    public bool finished = false;

    // Start is called before the first frame update
    void Start()
    {
        currenttime = startingtime;
        notifierText.gameObject.SetActive(false);
        Debug.Log(currenttime);

        // Initialize the code checker (can be set dynamically or through the inspector)
        //codeChecker = new ElonMuskCodeChecker(); // Example initialization
    }

    // Update is called once per frame
    void Update()
    {
        if (gameObject.activeInHierarchy)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;

            currenttime -= Time.unscaledDeltaTime;
            timerText.text = $"{currenttime:F0}s";
            //Debug.Log(currenttime);
            if (currenttime < 0)
            {
                currenttime = 0;
                finished = true;
                StartCoroutine(CloseWindowAfter2());
            }
        }
    }

    public void PlayMiniGame()
    {
        if (!finished)
        {
            this.gameObject.SetActive(true);
        }
    }    
    public void CheckCodeIfCorrect()
    {
        if (codeChecker.CheckCode(inputTextField.text))
        {
            StartCoroutine(ShowNotifierText(codeChecker.SuccessMessage, codeChecker.SuccessColor));
            StartCoroutine(CloseWindowAfter2());
            finished = true;
            if (TutorialManager.GetInstance.currentTutorial.Order == 9 ||
                TutorialManager.GetInstance.currentTutorial.Order == 15)
                StartCoroutine(TutorialManager.GetInstance.TutorialSuccess());
        }
        else
        {
            inputTextField.text = "";
            StartCoroutine(ShowNotifierText(codeChecker.FailureMessage, codeChecker.FailureColor));
        }
    }

    IEnumerator ShowNotifierText(string text, Color color)
    {
        notifierText.text = text;
        notifierText.color = color;
        notifierText.gameObject.SetActive(true);
        yield return new WaitForSeconds(3);
        notifierText.gameObject.SetActive(false);
    }

    IEnumerator CloseWindowAfter2()
    {
        yield return new WaitForSeconds(2);
        gameObject.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.RobotScene);
    }
}
