using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Bounce : MonoBehaviour
{
    private Rigidbody rb;

    [Header("Speed Settings")]
    public float wallSpeedMultiplier = 1.2f;   // sneller na muur hit
    public float groundSlowMultiplier = 0.8f;  // langzamer na grond hit
    public float maxSpeed = 15f;
    public float minSpeed = 1f;

    [Header("Bounce Settings")]
    public float extraBounceForce = 2f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            IncreaseSpeed();
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            DecreaseSpeed();
        }
    }

    private void IncreaseSpeed()
    {
        Vector3 direction = rb.linearVelocity.normalized;
        float currentSpeed = rb.linearVelocity.magnitude;

        float newSpeed = Mathf.Min(currentSpeed * wallSpeedMultiplier + extraBounceForce, maxSpeed);

        rb.linearVelocity = direction * newSpeed;
    }

    private void DecreaseSpeed()
    {
        Vector3 direction = rb.linearVelocity.normalized;
        float currentSpeed = rb.linearVelocity.magnitude;

        float newSpeed = Mathf.Max(currentSpeed * groundSlowMultiplier, minSpeed);

        rb.linearVelocity = direction * newSpeed;
    }
}