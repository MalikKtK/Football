using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f;
    private Transform ballTransform;
    public Vector3 startPosition { get; private set; }
    public Quaternion startRotation { get; private set; }

      // Metode kaldet ved start af spillet.
  void Start()
    {
        startPosition = transform.position;
        startRotation = transform.rotation;

        GameObject ball = GameObject.FindGameObjectWithTag("Ball");
        if (ball != null)
        {
            ballTransform = ball.transform;
        }
    }

       // Metode kaldt i hver opdateringsramme (frame).
        void Update()
    {
        if (ballTransform != null)
        {
            Vector3 direction = (ballTransform.position - transform.position).normalized;
            direction.y = 0;


            transform.position += direction * speed * Time.deltaTime;

            transform.LookAt(new Vector3(ballTransform.position.x, transform.position.y, ballTransform.position.z));
        }
    }

        // Metode til at nulstille positionen og rotationen for fjenden.
        public void ResetPosition()
    {
        transform.position = startPosition;
        transform.rotation = startRotation;

        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
