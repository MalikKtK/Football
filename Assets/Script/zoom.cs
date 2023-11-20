using UnityEngine;

public class ZoomInCamera : MonoBehaviour
{
    private float currentDistance;
    private float fullDistance;
    private float halfDistance;
    private bool isZoomedOut = true;
    void Start()
    {
        currentDistance = transform.position.y;
        fullDistance = currentDistance;
        halfDistance = currentDistance / 1.5f;
    }

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
            transform.position = new Vector3(transform.position.x, fullDistance, transform.position.z);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, halfDistance, transform.position.z);
        }
    }
}