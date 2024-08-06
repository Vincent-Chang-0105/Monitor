using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Objective01 : MonoBehaviour
{
    [Header("Components")]
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

    }

    public void StartPC()
    { 
        if(!ifDoneFirst)
        {
            StartCoroutine(missionObj());
            ifDoneFirst = true;
        }
    }
    private IEnumerator missionObj()
    {
        if(objSFX != null)
        {
            objSFX.Play();
        }
        //theObjective.SetActive(true);  
        animator.Play("ObjectiveDisplayAnim");
        objectiveText.text = "> Open up your pc and press start";     
        yield return new WaitForSeconds(3.5f);
        objectiveText.text = "";
        //theObjective.SetActive(false);
    }
}
