using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hider : MonoBehaviour
{
    private float lerpSpeed = 0.8f;
    // Update is called once per frame
    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject != null) return;

        //this.transform.position = Waypoint.current.transform.position;
        Vector3 currentPosition = transform.position;
        Vector3 newTargetPosition = HidingSpots.current.transform.position;

        // Interpolate between the current position and the target position
        this.transform.position = Vector3.Lerp(
            currentPosition,
            new Vector3(newTargetPosition.x, currentPosition.y, newTargetPosition.z),
            lerpSpeed * Time.deltaTime
        );

    }

}
