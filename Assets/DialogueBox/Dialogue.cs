using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Dialogue : MonoBehaviour
{
    public int Order;
    public TextMeshProUGUI textComponent;
    public string[] lines;
    public float textSpeed;
    public float lineDelay = 2f; // Delay before starting the next line (not used now)
    public AudioClip typingSound; // Sound effect for typing
    private AudioSource audioSource;

    private int index;
    private bool isTyping; // Flag to check if typing is in progress

    private RectTransform rectTransform;

    void Start()
    {
        textComponent.text = string.Empty;
        audioSource = GetComponent<AudioSource>(); // Get AudioSource component from GameObject
        rectTransform = GetComponent<RectTransform>(); // Get RectTransform component
        StartDialogue();
    }

    void Update()
    {
        if (this.isActiveAndEnabled)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            
        }

        if (Input.GetMouseButtonDown(0)) // Check for left mouse button click
        {
            Vector2 localMousePosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rectTransform, Input.mousePosition, null, out localMousePosition);

            if (rectTransform.rect.Contains(localMousePosition)) // Check if the mouse is within the bounds of the RectTransform
            {
                if (isTyping) // If text is still being typed
                {
                    StopAllCoroutines(); // Stop the typing coroutine
                    textComponent.text = lines[index]; // Show the entire line
                    isTyping = false; // Set flag to false
                }
                else
                {
                    NextLine(); // If not typing, go to the next line
                }
            }
        }
    }

    void StartDialogue()
    {
        index = 0;
        StartCoroutine(TypeLine());
        GameEventsManager.Instance.playerEvents.DisablePlayerMovement();
    }

    IEnumerator TypeLine()
    {
        isTyping = true; // Set flag to true when typing starts
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound); // Play typing sound effect
            }
            yield return new WaitForSeconds(textSpeed);
        }

        isTyping = false; // Set flag to false when typing is done
    }

    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());
        }
        else
        {
            gameObject.SetActive(false); // Deactivate the dialogue box when done
            GameEventsManager.Instance.playerEvents.EnablePlayerMovement(); 
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            if(Order == 0)
            {
                TutorialManager.GetInstance.SetNextTutorial(0);
            }
        }
    }
}
