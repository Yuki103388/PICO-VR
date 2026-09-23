using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform BallSpawnPoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();

            other.transform.position = BallSpawnPoint.position;
            other.transform.rotation = BallSpawnPoint.rotation;

            if (rb != null)
            {
                rb.linearVelocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }
        }
    }
}