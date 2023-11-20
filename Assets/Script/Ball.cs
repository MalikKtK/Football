using UnityEngine;

public class Ball : MonoBehaviour

{

    public bool isGrabbed = false; 
private Transform grabberTransform; // The transform of the player/AI grabbing the ball


    public void Grab(Transform grabber)
    {
        isGrabbed = true;
        grabberTransform = grabber;
        rb.isKinematic = true; // Disable physics
    }

    public void Release(Vector3 direction, float force)
    {
        isGrabbed = false;
        grabberTransform = null; // Clear the grabber reference
        rb.isKinematic = false; // Re-enable physics
        rb.AddForce(direction * force, ForceMode.VelocityChange); // Add force to throw the ball
    }

   void Update()
    {
        if (isGrabbed)
        {
            // Make the ball follow the grabber's position
            transform.position = grabberTransform.position + grabberTransform.forward * 0.5f; // Adjust this offset as needed
        }
    }
    public float initialSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        KickOff();
    }

    void KickOff()
    {
        // Give the ball an initial forward force
        rb.velocity = transform.forward * initialSpeed;
    }

    void OnCollisionEnter(Collision collision)
    {
        // You can add additional collision handling logic here if needed
    }
}