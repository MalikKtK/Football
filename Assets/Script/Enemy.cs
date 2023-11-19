using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    private Transform ballTransform;
    public Vector3 startPosition { get; private set; }
    public Quaternion startRotation { get; private set; }

    void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        // Find and store a reference to the ball's transform
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballTransform = ball.transform;
        }
    }

    void Update()
    {
        // Move towards the ball if it's been found
        if (ballTransform != null)
        {
            Vector3 direction = (ballTransform.position - transform.position).normalized;
            // Make sure the enemy doesn't move up or down
            direction.y = 0;

            // Move the enemy towards the ball
            transform.position += direction * speed * Time.deltaTime;

            // Optional: Make the enemy face the ball
            transform.LookAt(new Vector3(ballTransform.position.x, transform.position.y, ballTransform.position.z));
        }
    }

    public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;

        // Optionally reset any other components, like Rigidbody
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
