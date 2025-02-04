using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Robot UI")]
    [SerializeField] GameObject robotUI;

    [Header("Task - Panel")]
    [SerializeField] GameObject taskPanel;
    [SerializeField] LeanTweenType easeType;
    [SerializeField] private Button taskButton;
    private CanvasGroup canvastaskpanel;

    [Header("Bank Panel")]
    [SerializeField] GameObject Bank;
    [SerializeField] private TextMeshProUGUI moneyText;

    [Header("Task Bar")]
    [SerializeField] GameObject TaskBar;
    [SerializeField] GameObject BankButton;
    [SerializeField] GameObject HomeButton;

    [Header("Home Warning")]
    [SerializeField] GameObject HomeWarning;

    [Header("UI Buttons")]
    [SerializeField] GameObject GalleryButton;
    [SerializeField] GameObject QuestionMarkButton;
    [SerializeField] GameObject LeftMoveButton;
    [SerializeField] GameObject RightMoveButton;
    [SerializeField] GameObject CameraButton;

    [Header("Inventory UI")]
    [SerializeField] GameObject InventoryUI;
    [SerializeField] private Button exitInventoryButton;

    [Header("Camera UI")]
    [SerializeField] GameObject CameraUI;

    [Header("TaskLog UI")]
    [SerializeField] GameObject TaskLogUI;
    [SerializeField] GameObject exitTaskLogUIButton;

    [Header("Timer")]
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI dayText;

    private bool isHuman;
    private bool isEvadeMinigame;

    private void Start()
    {
        canvastaskpanel = taskPanel.GetComponent<CanvasGroup>();
        taskButton.onClick.AddListener(ToggleTaskPanel);
        GalleryButton.GetComponent<Button>().onClick.AddListener(ToggleInventory);
        exitInventoryButton.onClick.AddListener(ToggleInventory);
        BankButton.GetComponent<Button>().onClick.AddListener(ToggleBankPanel);
        exitTaskLogUIButton.GetComponent<Button>().onClick.AddListener(ToggleTaskLogUI);
        //CameraButton.GetComponent<Button>().onClick.AddListener(TakePicture);
    }
    private void OnEnable()
    {
        GameEventsManager.Instance.moneyEvents.onMoneyChange += MoneyChange;
        //GameEventsManager.Instance.playerEvents.onDisablePlayerMovement
        GameEventsManager.Instance.gameEvents.onStateChange += GameEvents_onStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.moneyEvents.onMoneyChange -= MoneyChange;
        GameEventsManager.Instance.gameEvents.onStateChange -= GameEvents_onStateChange;
    }

    private void GameEvents_onStateChange(GameState state)
    {
        isHuman = state == GameState.HumanScene || state == GameState.decipherMiniGameCafe || state == GameState.decipherMiniGameElson || state == GameState.evadeMiniGame;
        isEvadeMinigame = state == GameState.evadeMiniGame;
        SetActiveObject(GalleryButton, !isHuman);
        SetActiveObject(QuestionMarkButton, !isHuman);
        SetActiveObject(taskButton.gameObject, !isHuman);
        SetActiveObject(TaskBar, !isHuman);
        SetActiveObject(CameraUI, !isHuman);

        if(isEvadeMinigame)
        {
            if(timerText.alpha == 1)
            {
                ToggleTimerTexts();
            }
        }
        if(!isEvadeMinigame)
        {
            if(timerText.alpha == 0)
            {
                ToggleTimerTexts();
            }
        }
    }

    private void Update()
    {
        if(isHuman)
        {

        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                TakePicture();
            }

            if (Input.GetKeyDown(KeyCode.I))
            {
                ToggleInventory();
            }

            if (Input.GetKeyDown(KeyCode.Q))
            {
                ToggleTaskPanel();
            }
        }
    }

    private void MoneyChange(int money)
    {
        moneyText.text = "balance : $" + money.ToString();
        Debug.Log("money UI changed to :" + moneyText.text);
    }

    private void TakePicture()
    {
        if (IsCanvasGroupActive(canvastaskpanel))
        {
            return;
        }

        if (Bank.activeInHierarchy)
        {
            return;
        }

        if (InventoryUI.activeInHierarchy)
        {
            return;
        }

        ToggleUIButtons();
        ToggleTaskBar();
        ToggleTimerTexts();
        GameEventsManager.Instance.miscEvents.TakePicture();
    }
    private void ToggleTaskPanel()
    {
        // Disable button interactability during animation
        taskButton.interactable = false;


        if (!IsCanvasGroupActive(canvastaskpanel))
        {
            ActivateCanvasGroup(canvastaskpanel);
            LeanTween.moveX(taskButton.gameObject, 1345f, 0.2f).setEase(easeType).setOnComplete(() =>
            {
                ToggleUIButtons();
                ToggleCameraUI();
                ToggleTimerTexts();
                taskButton.interactable = true; // Enable button after animation
                taskButton.gameObject.SetActive(true);
            });
            LeanTween.moveX(taskPanel, 1665f, 0.2f).setEase(easeType);
        }
        else
        {
            LeanTween.moveX(taskButton.gameObject, 1853f, 0.2f).setEase(easeType).setOnComplete(() =>
            {
                taskButton.interactable = true; // Enable button after animation
            });
            LeanTween.moveX(taskPanel, 2170f, 0.2f).setEase(easeType).setOnComplete(() =>
            {
                DeactivateCanvasGroup(canvastaskpanel);
                ToggleUIButtons();
                ToggleCameraUI();
                ToggleTimerTexts();
                taskButton.gameObject.SetActive(true);
            });
        }
    }

    private void ToggleBankPanel()
    {
        ToggleUIButtons();
        ToggleCameraUI();
        ToggleTimerTexts();

        if (Bank.activeInHierarchy)
        {
            SetActiveObject(Bank, false);
            //GameEventsManager.Instance.PlayerEvents.DisablePlayerMovement();
        }
        else
        {
            SetActiveObject(Bank, true);
        }
    }

    private void ToggleUIButtons()
    {
        if (GalleryButton.activeInHierarchy)
        {
            //SetActiveObject(CameraButton, false);
            SetActiveObject(GalleryButton, false);
            SetActiveObject(QuestionMarkButton, false);
            //SetActiveObject(LeftMoveButton, false);
            //SetActiveObject(RightMoveButton, false);
            SetActiveObject(taskButton.gameObject, false);
        }
        else
        {
            //SetActiveObject(CameraButton, true);
            SetActiveObject(GalleryButton, true);
            SetActiveObject(QuestionMarkButton, true);
            //SetActiveObject(LeftMoveButton, true);
            //SetActiveObject(RightMoveButton, true);
            SetActiveObject(taskButton.gameObject, true);
        }
    }

    private void ToggleInventory()
    {
        if (IsCanvasGroupActive(canvastaskpanel))
        {
            return;
        }

        if (Bank.activeInHierarchy)
        {
            return;
        }

        ToggleUIButtons();
        ToggleTaskBar();
        ToggleCameraUI();
        ToggleTimerTexts();

        if (InventoryUI.activeInHierarchy)
        {
            SetActiveObject(InventoryUI, false);
        }
        else
        {
            SetActiveObject(InventoryUI, true);
        }
    }
    private void ToggleQuestionMark()
    {
        Debug.Log("TANONG KA JAN");
    }

    private void ToggleTaskBar()
    {
        if(TaskBar.activeInHierarchy)
        {
            SetActiveObject(TaskBar, false);
        }
        else
        {  SetActiveObject(TaskBar, true);}
    }

    private void ToggleCameraUI()
    {
        if (CameraUI.activeInHierarchy)
        {
            SetActiveObject(CameraUI, false);
        }
        else
        { SetActiveObject(CameraUI, true); }
    }

    private void ToggleTaskLogUI()
    {
        if(TaskLogUI.activeInHierarchy)
        {
            SetActiveObject(TaskLogUI, false);
        }
    }

    private void ToggleTimerTexts()
    {
        if(timerText.alpha == 0)
        {
            timerText.alpha = 1;
            dayText.alpha = 1;
        }
        else
        {
            timerText.alpha = 0;
            dayText.alpha = 0;
        }
    }
    private void SetActiveObject(GameObject obj, bool setActive)
    {
        obj.SetActive(setActive);
    }
    // Method to check if a CanvasGroup is active
    private bool IsCanvasGroupActive(CanvasGroup canvasgroup)
    {
        return canvasgroup.alpha > 0;
    }

    // Method to activate a CanvasGroup
    private void ActivateCanvasGroup(CanvasGroup canvasgroup)
    {
        canvasgroup.alpha = 1;
        canvasgroup.interactable = true;
        canvasgroup.blocksRaycasts = true;
    }

    // Method to deactivate a CanvasGroup
    private void DeactivateCanvasGroup(CanvasGroup canvasgroup)
    {
        canvasgroup.alpha = 0;
        canvasgroup.interactable = false;
        canvasgroup.blocksRaycasts = false;
    }

    public void ChangeToHuman()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        GameEventsManager.Instance.playerEvents.EnablePlayerMovement();
        GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.HumanScene);
    }
}
