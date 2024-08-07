using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ColliderVisualizer : MonoBehaviour
{
    private BoxCollider boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider>();
    }

    void Update()
    {
        if (boxCollider != null)
        {
            DrawBoxCollider();
        }
    }

    void DrawBoxCollider()
    {
        Vector3 boxCenter = transform.position + boxCollider.center;
        Vector3 halfSize = boxCollider.size * 0.5f;

        // Calculate the corners of the box
        Vector3[] corners = new Vector3[8];
        corners[0] = boxCenter + new Vector3(-halfSize.x, -halfSize.y, -halfSize.z);
        corners[1] = boxCenter + new Vector3(halfSize.x, -halfSize.y, -halfSize.z);
        corners[2] = boxCenter + new Vector3(halfSize.x, -halfSize.y, halfSize.z);
        corners[3] = boxCenter + new Vector3(-halfSize.x, -halfSize.y, halfSize.z);
        corners[4] = boxCenter + new Vector3(-halfSize.x, halfSize.y, -halfSize.z);
        corners[5] = boxCenter + new Vector3(halfSize.x, halfSize.y, -halfSize.z);
        corners[6] = boxCenter + new Vector3(halfSize.x, halfSize.y, halfSize.z);
        corners[7] = boxCenter + new Vector3(-halfSize.x, halfSize.y, halfSize.z);

        // Draw the edges of the box
        Debug.DrawLine(corners[0], corners[1], Color.green);
        Debug.DrawLine(corners[1], corners[2], Color.green);
        Debug.DrawLine(corners[2], corners[3], Color.green);
        Debug.DrawLine(corners[3], corners[0], Color.green);

        Debug.DrawLine(corners[4], corners[5], Color.green);
        Debug.DrawLine(corners[5], corners[6], Color.green);
        Debug.DrawLine(corners[6], corners[7], Color.green);
        Debug.DrawLine(corners[7], corners[4], Color.green);

        Debug.DrawLine(corners[0], corners[4], Color.green);
        Debug.DrawLine(corners[1], corners[5], Color.green);
        Debug.DrawLine(corners[2], corners[6], Color.green);
        Debug.DrawLine(corners[3], corners[7], Color.green);
    }
}
