using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(Rigidbody))]
public class Bounce : MonoBehaviour
{
    private Rigidbody rb;
    private XRGrabInteractable grabInteractable;

    [Header("Speed Settings")]
    public float wallSpeedMultiplier = 1.2f;
    public float groundSlowMultiplier = 0.8f;
    public float maxSpeed = 15f;
    public float minSpeed = 1f;

    [Header("Bounce Settings")]
    public float extraBounceForce = 2f;

    [Header("Anti Stuck")]
    public float stuckSpeed = 0.3f;
    public float stuckTime = 1.5f;
    public float unstuckForce = 2f;

    private float stuckTimer;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grabInteractable = GetComponent<XRGrabInteractable>();
    }

    private void FixedUpdate()
    {
        CheckIfStuck();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            IncreaseSpeed();
            Renderer wallRenderer = collision.gameObject.GetComponentInChildren<Renderer>();
            if (wallRenderer != null)
            {
                wallRenderer.material.color = Random.ColorHSV();
            }
        }

        if (collision.gameObject.CompareTag("Ground"))
        {
            DecreaseSpeed();
        }
    }

    private void IncreaseSpeed()
    {
        float currentSpeed = rb.linearVelocity.magnitude;
        Vector3 direction = rb.linearVelocity.normalized;

        if (direction == Vector3.zero)
        {
            direction = Random.insideUnitSphere.normalized;
        }

        float newSpeed = Mathf.Min(
            currentSpeed * wallSpeedMultiplier + extraBounceForce,
            maxSpeed
        );

        rb.linearVelocity = direction * newSpeed;
    }

    private void DecreaseSpeed()
    {
        float currentSpeed = rb.linearVelocity.magnitude;

        if (currentSpeed < 0.05f)
            return;

        Vector3 direction = rb.linearVelocity.normalized;

        float newSpeed = Mathf.Max(
            currentSpeed * groundSlowMultiplier,
            minSpeed
        );

        rb.linearVelocity = direction * newSpeed;
    }

    private void CheckIfStuck()
    {
        // Bal wordt vastgehouden
        if (grabInteractable != null && grabInteractable.isSelected)
        {
            stuckTimer = 0f;
            return;
        }

        // Bal beweegt bijna niet
        if (rb.linearVelocity.magnitude < stuckSpeed)
        {
            stuckTimer += Time.fixedDeltaTime;

            if (stuckTimer >= stuckTime)
            {
                UnstuckBall();
                stuckTimer = 0f;
            }
        }
        else
        {
            stuckTimer = 0f;
        }
    }

    private void UnstuckBall()
    {
        Vector3 randomDirection = new Vector3(
            Random.Range(-1f, 1f),
            1f,
            Random.Range(-1f, 1f)
        ).normalized;

        rb.AddForce(
            randomDirection * unstuckForce,
            ForceMode.Impulse
        );
    }
}