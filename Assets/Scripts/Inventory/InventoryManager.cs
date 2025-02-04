using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryManager : MonoBehaviour
{
    [Header("Inventory Canvas")]
    [SerializeField]public GameObject InventoryCanvas;

    [Header("Inventory Slots")]
    [SerializeField] ItemSlot[] ItemSlot;

    [Header("Camera - Canvas")]
    [SerializeField]public GameObject CameraCanvas;

    [Header("UI - Canvas")]
    [SerializeField] public GameObject UICanvas;

    private static List<Sprite> photoSprites = new List<Sprite>();

    public bool menuActivated = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null) return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            //toggleInventory();
        }
    }
    public void toggleInventory()
    {
        InventoryCanvas.SetActive(!menuActivated);
        CameraCanvas.SetActive(menuActivated);
        UICanvas.SetActive(menuActivated);
        menuActivated = !menuActivated;
    }
    public void AddPhotoSprite(Sprite photoSprite)
    {
        for (int i = 0; i < ItemSlot.Length; i++)
        {
            if (!ItemSlot[i].hasPhoto)
            {
                ItemSlot[i].AddPhotoSprite(photoSprite);
                Debug.Log($"Photo added to slot {i}");
                return;
            }
        }
        Debug.LogWarning("No empty slot available for the photo.");
    }

    public void ClearPhotoSprite()
    {
        photoSprites.Clear();
        Debug.Log("Photo Album cleared.");
    }
}
