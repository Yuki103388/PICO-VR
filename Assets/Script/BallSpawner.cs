using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    public GameObject[] ballPrefabs;

    private GameObject currentBall;

    void Start()
    {
        SpawnRandomBall();
    }

    public void SpawnRandomBall()
    {
        int randomIndex = Random.Range(0, ballPrefabs.Length);

        currentBall = Instantiate(
            ballPrefabs[randomIndex],
            transform.position,
            transform.rotation
        );
    }

    public void RespawnBall(GameObject oldBall)
    {
        Destroy(oldBall);

        SpawnRandomBall();
    }
}