using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BallCatchScore : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private BallSpawner ballSpawner;

    private bool alreadyScored;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnCaught);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    private void Start()
    {
        ballSpawner = FindFirstObjectByType<BallSpawner>();
    }

    private void OnCaught(SelectEnterEventArgs args)
    {
        if (alreadyScored)
            return;

        alreadyScored = true;

        // Score verhogen
        ScoreManager.instance.AddScore(1);

        // Controleren of een nieuwe bal nodig is
        if (ballSpawner != null)
        {
            ballSpawner.UpdateBallForScore(
                ScoreManager.instance.score
            );
        }
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        alreadyScored = false;

        // Controleren of de bal vervangen moet worden
        if (ballSpawner != null)
        {
            ballSpawner.OnBallReleased(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectEntered.RemoveListener(OnCaught);
            grabInteractable.selectExited.RemoveListener(OnReleased);
        }
    }
}