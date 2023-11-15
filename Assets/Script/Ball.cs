using UnityEngine;

public class Ball : MonoBehaviour
{
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