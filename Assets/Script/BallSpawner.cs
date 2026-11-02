using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Balls")]
    public GameObject[] ballPrefabs;

    [Header("Score Settings")]
    public int CatchesPerBall = 5;

    private GameObject currentBall;

    private int currentBallIndex = 0;
    private int nextBallIndex = 0;

    void Start()
    {
        SpawnBall();
    }

    private void SpawnBall()
    {
        if (ballPrefabs == null || ballPrefabs.Length == 0)
            return;

        currentBall = Instantiate(
            ballPrefabs[currentBallIndex],
            transform.position,
            transform.rotation
        );
    }

    // Wordt aangeroepen wanneer de speler punten krijgt
    public void UpdateBallForScore(int score)
    {
        if (ballPrefabs == null || ballPrefabs.Length == 0)
            return;

        nextBallIndex = Mathf.Min(
            Mathf.Max(0, score) / Mathf.Max(1, CatchesPerBall),
            ballPrefabs.Length - 1
        );
    }

    public void RespawnBall(GameObject oldBall)
    {
        if (oldBall != currentBall)
            return;

        Destroy(oldBall);

        // Nieuwe bal kiezen op basis van de score
        currentBallIndex = nextBallIndex;

        SpawnBall();
    }
}