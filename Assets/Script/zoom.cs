using UnityEngine;

public class ZoomInCamera : MonoBehaviour
{
    private float currentDistance;
    private float fullDistance;
    private float halfDistance;
    private bool isZoomedOut = true;

    // Start is called before the first frame update
    void Start()
    {
        currentDistance = transform.position.y;
        fullDistance = currentDistance;
        halfDistance = currentDistance / 1.5f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            ToggleZoom();
        }
    }

    void ToggleZoom()
    {
        isZoomedOut = !isZoomedOut;

        if (isZoomedOut)
        {
            // Zoom out to full distance
            transform.position = new Vector3(transform.position.x, fullDistance, transform.position.z);
        }
        else
        {
            // Zoom in to half distance
            transform.position = new Vector3(transform.position.x, halfDistance, transform.position.z);
        }
    }
}