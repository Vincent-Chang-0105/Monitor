using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakePictureQuest : QuestStep
{
    private int picturesTook = 0;
    private int picturesToTake = 1;
    private BoxCollider taskCollider;

    private void OnEnable()
    {
        GameEventsManager.Instance.miscEvents.OnPictureTaken += PictureTaken;

        // Find the BoxCollider of the task object
        GameObject taskObject = GameObject.Find("Task 1");

        if (taskObject != null)
        {
            taskCollider = taskObject.GetComponent<BoxCollider>();
            if (taskCollider != null)
            {
                Debug.Log("Found area task 1 with BoxCollider");
            }
            else
            {
                Debug.LogWarning("Task 1 does not have a BoxCollider");
            }
        }
        else
        {
            Debug.LogWarning("Task 1 GameObject not found");
        }
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.miscEvents.OnPictureTaken -= PictureTaken;
    }

    private void PictureTaken()
    {
        if (taskCollider != null && IsPlayerInCollider())
        {
            if (picturesTook < picturesToTake)
            {
                picturesTook++;
            }

            if (picturesTook >= picturesToTake)
            {
                FinishQuestStep();
            }
        }
    }

    private bool IsPlayerInCollider()
    {
        // Assuming the player has a Collider component
        Collider playerCollider = GameObject.FindWithTag("Player").GetComponent<Collider>();

        if (playerCollider != null)
        {
            return taskCollider.bounds.Intersects(playerCollider.bounds);
        }
        return false;
    }
}
