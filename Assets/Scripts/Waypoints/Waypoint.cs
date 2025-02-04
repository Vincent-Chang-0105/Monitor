using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Waypoint : MonoBehaviour, IPointerClickHandler
{
    public static Waypoint current = null;

    [SerializeField] private bool is_starting = false;
    [SerializeField] private List<Waypoint> connected;

    private void Awake()
    {
        if (is_starting)
        {
            current = this;
        }

        // Start the floating animation
        StartFloatingAnimation();
    }

    // Function to start the floating animation
    private void StartFloatingAnimation()
    {
        // LeanTween floating animation
        LeanTween.moveLocalY(gameObject, transform.localPosition.y + 0.1f, 1f).setLoopPingPong();
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (this == current)
            return;

        foreach (var waypoint in current.connected) 
        {
            if (waypoint == this)
            {
                return;
            }    
        }

        this.gameObject.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("waypoint clicked");
        foreach (var otherwaypoint in current.connected)
        {
            if(this == otherwaypoint)
            {
                foreach (var oldconnected in current.connected)
                {
                    oldconnected.gameObject.SetActive(false);
                }

                current = this;

                foreach (var newconnected in current.connected)
                {
                    newconnected.gameObject.SetActive(true);
                }

                return;
            }
        }
    }
}
