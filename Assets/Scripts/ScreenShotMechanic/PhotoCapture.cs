using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PhotoCapture : MonoBehaviour
{
    [Header("Photo Taker")]
    [SerializeField] private Image photoDisplayArea;
    [SerializeField] private GameObject photoFrame;
    [SerializeField] private GameObject cameraUI;
    [SerializeField] TextMeshProUGUI ShotsRemainingDisplay;
    [SerializeField] private TextMeshProUGUI notifier;

    [Header("Flash Effect")]
    [SerializeField] private GameObject cameraFlash;
    [SerializeField] private float flashTime;

    [Header("Photo Fader Effect")]
    [SerializeField] private Animator fadingAnimation;

    [Header("Cooldown Settings")]
    [SerializeField] private float photoCooldown = 2f; // Cooldown period between taking photos
    private float lastPhotoTime; // Time when the last photo was taken

    [Header("Audio")]
    [SerializeField] private AudioSource cameraAudio;

    [Header("Inventory Manager")]
    [SerializeField] private InventoryManager inventoryManager;

    [Header("Waypoints")]
    [SerializeField] private GameObject wayPoints;

    [Header("Task 1 Bounds")]
    [SerializeField] private Collider taskCollider1;

    [Header("Task 2 Bounds")]
    [SerializeField] private Collider taskCollider2;

    public Collider Player;

    private Texture2D screenCapture;
    private bool viewingPhoto;
    private int shotsRemaining;
    private int shotsAvailable = 6;

    private void Start()
    {
        shotsRemaining = shotsAvailable;

        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

        ShotsRemainingDisplay.text = $"{(shotsAvailable - shotsRemaining)}/{shotsAvailable}";

    }

    private void OnEnable()
    {
        GameEventsManager.Instance.miscEvents.onTakePicture += TakePhoto;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.miscEvents.onTakePicture -= TakePhoto;
    }

    private void Update()
    {
    }

    private void TakePhoto()
    {
        if (shotsRemaining != 0)
        {
            if (!viewingPhoto && Time.time - lastPhotoTime > photoCooldown)
            {
                StartCoroutine(CapturePhoto());
                lastPhotoTime = Time.time; // Update the last photo time
                shotsRemaining--;
                ShotsRemainingDisplay.text = $"{(shotsAvailable - shotsRemaining)}/{shotsAvailable}";
                viewingPhoto = true;
            }
            else if (viewingPhoto)
            {
                viewingPhoto = false;
                RemovePhoto();
            }
        }
    }
    private IEnumerator CapturePhoto()
    {
        cameraUI.SetActive(false);
        wayPoints.SetActive(false);
        yield return new WaitForEndOfFrame();

        Rect regionToRead = new Rect (0, 0, Screen.width,Screen.height);

        screenCapture.ReadPixels(regionToRead, 0, 0, false);
        screenCapture.Apply();
        ShowPhoto();
    }

    private void ShowPhoto()
    {
        Sprite photoSprite = Sprite.Create(screenCapture, new Rect(0.0f, 0.0f, screenCapture.width, screenCapture.height), new Vector2(0.5f, 0.5f), 100.0f);
        photoDisplayArea.sprite = photoSprite;

        inventoryManager.AddPhotoSprite(photoSprite);

        photoFrame.SetActive(true);
        StartCoroutine(CameraFlashEffect());
        fadingAnimation.Play("PhotoFade");

        LeanTween.alphaCanvas(notifier.GetComponent<CanvasGroup>(), 1f, 1f).setDelay(1f);
        //ADD THIS
        screenCapture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        GameEventsManager.Instance.miscEvents.PictureTook();
    }

    private IEnumerator CameraFlashEffect()
    {
        cameraAudio.Play();
        cameraFlash.SetActive(true);
        yield return new WaitForSeconds(flashTime);
        cameraFlash.SetActive(false);
    }
    private void RemovePhoto()
    {
        photoFrame.SetActive(false);
        cameraUI.SetActive(true);
        wayPoints.SetActive(true);
        notifier.GetComponent<CanvasGroup>().alpha = 0f;

        if(IsPlayerInCollider(taskCollider1))
        {
            Debug.Log("decipher 1");
            GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.decipherMiniGameCafe);
        }

        if (IsPlayerInCollider(taskCollider2))
        {
            Debug.Log("decipher 2");
            GameEventsManager.Instance.gameEvents.UpdateGameState(GameState.decipherMiniGameElson);
        }
    }
    private bool IsPlayerInCollider(Collider other)
    {
        if (Player != null)
        {
            return other.bounds.Intersects(Player.bounds);
        }
        return false;
    }
}
