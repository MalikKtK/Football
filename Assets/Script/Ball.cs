using UnityEngine;

public class Ball : MonoBehaviour

{

    public bool isGrabbed = false; 
private Transform grabberTransform;


    // Metode kaldet når bolden grebes.
    public void Grab(Transform grabber)
    {
        isGrabbed = true;
        grabberTransform = grabber;
        rb.isKinematic = true;
    }

      // Metode kaldet når bolden slippes.
  public void Release(Vector3 direction, float force)
    {
        isGrabbed = false;
        grabberTransform = null;
        rb.isKinematic = false;
        rb.AddForce(direction * force, ForceMode.VelocityChange);
    }

    // Metode kaldet i hver opdateringsramme (frame) når bolden er grebet.
  void Update()
    {
        if (isGrabbed)
        {
            transform.position = grabberTransform.position + grabberTransform.forward * 0.5f;
        }
    }
    public float initialSpeed = 5f;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        KickOff();
    }

    // Metode til at sparke bolden ved start.
    void KickOff()
    {
        rb.velocity = transform.forward * initialSpeed;
    }

       // Metode kaldt når bolden kolliderer med noget.
        void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("AIPlayer"))
        {
            Vector3 direction = (collision.transform.position - transform.position).normalized;
            direction.y = 0;

            collision.gameObject.GetComponent<Rigidbody>().AddForce(direction * 5f, ForceMode.VelocityChange);
        }
    }
}