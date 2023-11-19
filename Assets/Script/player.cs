using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float respawnWaitTime = 2.0f;
    public Vector3 startPosition { get; private set; }
    public Quaternion startRotation { get; private set; }
    public float maxSpeed = 4f;
    public float timeToMax = .26f;
    private float GainPerSecond { get => maxSpeed / timeToMax; }

    public float timeToStop = .18f;
    private float LossPerSecond { get => maxSpeed / timeToStop; }

    public float reverseMomentum = 2.2f;
    public float rotationSpeed = .1f;

    private Vector3 movement = Vector3.zero;
    private bool canMove = true;

    public CharacterController movingPoint;
    public Transform rotationPoint;

    public float fixedYPosition = 20.0f;

     public float speed = 5f; // Speed at which the player moves towards the ball
    private Transform ballTransform;

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

    public void ResetPosition()
    {
        if (movingPoint != null)
        {
            movingPoint.enabled = false; // Disable CharacterController
        }

        transform.position = startPosition;
        transform.rotation = startRotation;

        if (movingPoint != null)
        {
            movingPoint.enabled = true; // Re-enable CharacterController
        }

        canMove = false; // Disable movement temporarily
        StartCoroutine(EnableMovementAfterDelay(respawnWaitTime));
    }

    IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true; // Enable movement
    }

    // Update is called once per frame
    
    void Update()
    {
        if (!canMove) return; // Skip movement logic if canMove is false

        // AI Movement towards the ball
        if (ballTransform != null)
        {
            Vector3 direction = (ballTransform.position - transform.position).normalized;
            direction.y = 0; // Keep y-axis movement to zero

            // Move the player towards the ball using CharacterController
            if (movingPoint != null)
            {
                movingPoint.Move(direction * speed * Time.deltaTime);
            }

            // Make the player face the ball
            if (rotationPoint != null)
            {
                rotationPoint.rotation = Quaternion.Slerp(
                    rotationPoint.rotation,
                    Quaternion.LookRotation(direction),
                    rotationSpeed
                );
            }
        }

        // Up/Down (X-axis)
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            // Up-slope Positive
            if (movement.x >= 0)
            {
                movement.x += GainPerSecond * Time.deltaTime;
                if (movement.x > maxSpeed) movement.x = maxSpeed;
            }
            else
            {
                // Break-slope Negative
                movement.x += GainPerSecond * reverseMomentum * Time.deltaTime;
                if (movement.x > 0) movement.x = 0;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            // Down-slope Negative
            if (movement.x <= 0)
            {
                movement.x -= GainPerSecond * Time.deltaTime;
                if (movement.x < -maxSpeed) movement.x = -maxSpeed;
            }
            else
            {
                // Break-slope Positive
                movement.x -= GainPerSecond * reverseMomentum * Time.deltaTime;
                if (movement.x < 0) movement.x = 0;
            }
        }
        else
        {
            if (movement.x > 0)
            {
                // Fadeout from Positive
                movement.x -= LossPerSecond * Time.deltaTime;
                if (movement.x < 0) movement.x = 0;
            }
            else if (movement.x < 0)
            {
                // Fadeout from Negative
                movement.x += LossPerSecond * Time.deltaTime;
                if (movement.x > 0) movement.x = 0;
            }
        }

        // Left/Right (Y-axis)
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            // Up-slope Positive
            if (movement.z >= 0)
            {
                movement.z += GainPerSecond * Time.deltaTime;
                if (movement.z > maxSpeed) movement.z = maxSpeed;
            }
            else
            {
                // Break-slope Negative
                movement.z += GainPerSecond * reverseMomentum * Time.deltaTime;
                if (movement.z > 0) movement.z = 0;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            // Down-slope Negative
            if (movement.z <= 0)
            {
                movement.z -= GainPerSecond * Time.deltaTime;
                if (movement.z < -maxSpeed) movement.z = -maxSpeed;
            }
            else
            {
                // Break-slope Positive
                movement.z -= GainPerSecond * reverseMomentum * Time.deltaTime;
                if (movement.z < 0) movement.z = 0;
            }
        }
        else
        {
            if (movement.z > 0)
            {
                // Fadeout from Positive
                movement.z -= LossPerSecond * Time.deltaTime;
                if (movement.z < 0) movement.z = 0;
            }
            else if (movement.z < 0)
            {
                // Fadeout from Negative
                movement.z += LossPerSecond * Time.deltaTime;
                if (movement.z > 0) movement.z = 0;
            }
        }

        if (movement.x != 0 || movement.z != 0)
        {
            // Only move when necessary
            movingPoint.Move(movement * Time.deltaTime);
            rotationPoint.rotation = Quaternion.Slerp(
                rotationPoint.rotation,
                Quaternion.LookRotation(movement),
                rotationSpeed
            );
        }
    }

    // LateUpdate is called once per frame after all Update functions have been called
   void LateUpdate()
    {
        // Keep the y-coordinate fixed at a specific value
        Vector3 newPosition = movingPoint.transform.position;
        newPosition.y = fixedYPosition;
        movingPoint.transform.position = newPosition;
    }
}
