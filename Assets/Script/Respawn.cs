using UnityEngine;

public class Respawn : MonoBehaviour
{
    public BallSpawner ballSpawner;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ball"))
        {
            ballSpawner.RespawnBall(other.gameObject);
        }
    }
}