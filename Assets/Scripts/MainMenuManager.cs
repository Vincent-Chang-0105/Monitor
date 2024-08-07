using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainCanvas;
    [SerializeField] private GameObject chapterSelect;

    private void Start()
    {
        mainCanvas.SetActive(true);
        chapterSelect.SetActive(false);
    }
    public void LoadScene(string SceneName)
    {
        SceneManager.LoadScene(SceneName);
    }

    public void ExitScene()
    {
        Application.Quit();
    }

    public void OpenChapterSelect()
    {
        mainCanvas.SetActive(false);
        chapterSelect.SetActive(true);
    }    

    public void CloseChapterSelect()
    {
        chapterSelect.SetActive(false);
        mainCanvas.SetActive(true);
    }
}
