using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Objective1Completer : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private GameObject source;
    [SerializeField] private AudioSource objSFX;
    [SerializeField] private TextMeshProUGUI objectiveText;
    [SerializeField] private Animator animator;

    private bool ifDoneFirst = false;
    // Start is called before the first frame update

    // Update is called once per frame
    private void Start()
    {
    }
    void Update()
    {
        if(source.activeInHierarchy)
        {
            if (ifDoneFirst)
                return;
            StartCoroutine(missionObj());
            ifDoneFirst = true;
        }
    }

    private IEnumerator missionObj()
    {
        if (objSFX != null)
        {
            objSFX.Play();
        }
        //theObjective.SetActive(true);  
        animator.Play("ObjectiveDisplayAnim");
        objectiveText.text = "> Fire up the pc and press start - Complete";
        yield return new WaitForSeconds(3.5f);
        objectiveText.text = "";
        //theObjective.SetActive(false);
    }
}
