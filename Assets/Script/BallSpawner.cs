using UnityEngine;

public class BallSpawner : MonoBehaviour
{
    [Header("Balls")]
    public GameObject[] ballPrefabs;

    [Header("Score Settings")]
    public int CatchesPerBall = 5;

    private GameObject currentBall;

    private int currentBallIndex = 0;
    private bool replaceOnRelease = false;

    private void Start()
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

    // Controleren of de speler genoeg punten heeft
    public void UpdateBallForScore(int score)
    {
        if (ballPrefabs == null || ballPrefabs.Length == 0)
            return;

        if (score <= 0 || score % Mathf.Max(1, CatchesPerBall) != 0)
            return;

        // Volgende bal kiezen
        currentBallIndex++;

        // Opnieuw beginnen als alle ballen zijn geweest
        if (currentBallIndex >= ballPrefabs.Length)
        {
            currentBallIndex = 0;
        }

        // Bal vervangen zodra de speler hem loslaat
        replaceOnRelease = true;
    }

    // Wordt aangeroepen wanneer de speler de bal loslaat
    public void OnBallReleased(GameObject releasedBall)
    {
        if (!replaceOnRelease)
            return;

        if (releasedBall != currentBall)
            return;

        replaceOnRelease = false;

        RespawnBall(releasedBall);
    }

    public void RespawnBall(GameObject oldBall)
    {
        if (oldBall != currentBall)
            return;

        Destroy(oldBall);

        currentBall = null;

        replaceOnRelease = false;

        SpawnBall();
    }
}