using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float respawnWaitTime = 2.0f;
    private Vector3 spawnPoint;
    private Quaternion spawnRotation;
    private bool dead = false;

    public float maxSpeed = 4f;
    public float timeToMax = .26f;
    private float GainPerSecond { get => maxSpeed / timeToMax; }

    public float timeToStop = .18f;
    private float LossPerSecond { get => maxSpeed / timeToStop; }

    public float reverseMomentum = 2.2f;
    public float rotationSpeed = .1f;

    private Vector3 movement = Vector3.zero;

    public CharacterController movingPoint;
    public Transform rotationPoint;

    // Define the fixed y-position you want your character to stay at.
    public float fixedYPosition = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = movingPoint.transform.position;
        spawnRotation = rotationPoint.rotation;
    }

    // Update is called once per frame
    void Update()
    {
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
        Vector3 newPosition = transform.position;
        newPosition.y = fixedYPosition; // Use your desired fixed y-position here
        transform.position = newPosition;
    }

    public void Die()
    {
        if (!dead)
        {
            dead = true;
            // Stop our movement
            movement = Vector3.zero;
            // Stop calling Update
            enabled = false;
            // Stop registering collisions
            movingPoint.enabled = false;
            // Stop drawing the model
            rotationPoint.gameObject.SetActive(false);
            // Prepare to wake us up again
            Invoke(nameof(Respawn), respawnWaitTime);
        }
    }

    public void Respawn()
    {
        dead = false;
        // Set initial position and rotation
        movingPoint.transform.position = spawnPoint;
        rotationPoint.rotation = spawnRotation;
        // Enable the update script again
        enabled = true;
        // Enable collision detection
        movingPoint.enabled = true;
        // Start drawing the model again
        rotationPoint.gameObject.SetActive(true);
    }
}
