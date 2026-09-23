using UnityEngine;

public class Ball : MonoBehaviour
{
    public BallData Data;

    private Rigidbody rb;
    private Renderer ballRenderer;
    private Collider ballCollider;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        ballRenderer = GetComponent<Renderer>();
        ballCollider = GetComponent<Collider>();

        ApplyData();
    }

    void ApplyData()
    {
        if (Data == null)
            return;

        rb.mass = Data.Mass;
        rb.linearDamping = Data.LinearDamping;
        rb.angularDamping = Data.AngularDamping;

        if (Data.BallMaterial != null)
        {
            ballRenderer.material = Data.BallMaterial;
        }

        if (Data.PhysicsMaterial != null)
        {
            ballCollider.material = Data.PhysicsMaterial;
        }
    }
}