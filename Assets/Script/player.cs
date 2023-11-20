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

     public float speed = 5f;
    private Transform ballTransform;

    private Ball ballScript;
private bool hasBall = false;

        // Metode kaldt ved start af spillet.
void Start()
    {
        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballScript = ball.GetComponent<Ball>();
            ballTransform = ball.transform;
        }

        startPosition = transform.position;
        startRotation = transform.rotation;
    }


       // Metode til at nulstille spillerens position.
 public void ResetPosition()
    {
        if (movingPoint != null)
        {
            movingPoint.enabled = false;
        }

        transform.position = startPosition;
        transform.rotation = startRotation;

        if (movingPoint != null)
        {
            movingPoint.enabled = true;
        }

        canMove = false;
        StartCoroutine(EnableMovementAfterDelay(respawnWaitTime));
    }
    IEnumerator EnableMovementAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        canMove = true;
    }

    
         // Metode kaldt i hver opdateringsramme (frame).
 void Update()
    {


        if (ballTransform != null)
        {
            Vector3 direction = (ballTransform.position - transform.position).normalized;
            direction.y = 0;

            if (movingPoint != null)
            {
                movingPoint.Move(direction * speed * Time.deltaTime);
            }
 
            if (rotationPoint != null)
            {
                rotationPoint.rotation = Quaternion.Slerp(
                    rotationPoint.rotation,
                    Quaternion.LookRotation(direction),
                    rotationSpeed
                );
            }
        }

         if (Input.GetKeyDown(KeyCode.G))
        {
            TryGrabBall();
        }

        if (Input.GetKeyDown(KeyCode.H) && hasBall)
        {
            ReleaseBall();
        }

        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            if (movement.x >= 0)
            {
                movement.x += GainPerSecond * Time.deltaTime;
                if (movement.x > maxSpeed) movement.x = maxSpeed;
            }
            else
            {
                movement.x += GainPerSecond * reverseMomentum * Time.deltaTime;
                if (movement.x > 0) movement.x = 0;
            }
        }
        else if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            if (movement.x <= 0)
            {
                movement.x -= GainPerSecond * Time.deltaTime;
                if (movement.x < -maxSpeed) movement.x = -maxSpeed;
            }
            else
            {
                movement.x -= GainPerSecond * reverseMomentum * Time.deltaTime;
                if (movement.x < 0) movement.x = 0;
            }
        }
        else
        {
            if (movement.x > 0)
            {
                movement.x -= LossPerSecond * Time.deltaTime;
                if (movement.x < 0) movement.x = 0;
            }
            else if (movement.x < 0)
            {
                movement.x += LossPerSecond * Time.deltaTime;
                if (movement.x > 0) movement.x = 0;
            }
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if (movement.z >= 0)
            {
                movement.z += GainPerSecond * Time.deltaTime;
                if (movement.z > maxSpeed) movement.z = maxSpeed;
            }
            else
            {
                movement.z += GainPerSecond * reverseMomentum * Time.deltaTime;
                if (movement.z > 0) movement.z = 0;
            }
        }
        else if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            if (movement.z <= 0)
            {
                movement.z -= GainPerSecond * Time.deltaTime;
                if (movement.z < -maxSpeed) movement.z = -maxSpeed;
            }
            else
            {
                movement.z -= GainPerSecond * reverseMomentum * Time.deltaTime;
                if (movement.z < 0) movement.z = 0;
            }
        }
        else
        {
            if (movement.z > 0)
            {
                movement.z -= LossPerSecond * Time.deltaTime;
                if (movement.z < 0) movement.z = 0;
            }
            else if (movement.z < 0)
            {
                movement.z += LossPerSecond * Time.deltaTime;
                if (movement.z > 0) movement.z = 0;
            }
        }

        if (movement.x != 0 || movement.z != 0)
        {
            movingPoint.Move(movement * Time.deltaTime);
            rotationPoint.rotation = Quaternion.Slerp(
                rotationPoint.rotation,
                Quaternion.LookRotation(movement),
                rotationSpeed
            );
        }
    }
    // Metode til at håndtere AI-styret bevægelse.
private void AIMovement()
    {
        if (ballTransform != null && !ballScript.isGrabbed)
        {
            Vector3 direction = (ballTransform.position - transform.position).normalized;
            direction.y = 0;

            if (movingPoint != null)
            {
                movingPoint.Move(direction * speed * Time.deltaTime);
                movement = direction;
            }
        }
    }

      // Metode til at håndtere manuel bevægelse via tastatur eller lignende input.
  private void ManualMovement()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontal, 0, vertical).normalized;

        if (movingPoint != null)
        {
            movingPoint.Move(direction * speed * Time.deltaTime);
            movement = direction;
        }
    }

       // Metode til at håndtere interaktion med bolden.
 private void HandleBallInteraction()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            TryGrabBall();
        }

        if (Input.GetKeyDown(KeyCode.H) && hasBall)
        {
            ReleaseBall();
        }
    }

        // Metode til at forsøge at gribe bolden, hvis den er inden for rækkevidde.
private void TryGrabBall()
    {
        if (ballScript != null && !ballScript.isGrabbed && Vector3.Distance(ballScript.transform.position, transform.position) <= 2.0f)
        {
            ballScript.Grab(this.transform);
            hasBall = true;
        }
    }

        // Metode til at frigive bolden med en bestemt kraft og retning.
private void ReleaseBall()
    {
        if (hasBall)
        {
            float throwForce = 10f;
            ballScript.Release(this.transform.forward, throwForce);
            hasBall = false;
        }
    }

        // Metode til at opdatere spillerens rotation baseret på bevægelsesretningen.
private void UpdateRotation()
    {
        rotationPoint.rotation = Quaternion.Slerp(
            rotationPoint.rotation,
            Quaternion.LookRotation(movement),
            rotationSpeed
        );
    }

        // Metode kaldt sent i hver opdateringsramme (frame) til at justere Y-positionen.
void LateUpdate()
    {
        Vector3 newPosition = movingPoint.transform.position;
        newPosition.y = fixedYPosition;
        movingPoint.transform.position = newPosition;
    }
}


