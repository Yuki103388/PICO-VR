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
        // BallSpawner automatisch zoeken in de scene
        ballSpawner = FindFirstObjectByType<BallSpawner>();
    }

    private void OnCaught(SelectEnterEventArgs args)
    {
        if (alreadyScored)
            return;

        // Score verhogen
        ScoreManager.instance.AddScore(1);

        // Score doorgeven aan BallSpawner
        if (ballSpawner != null)
        {
            ballSpawner.UpdateBallForScore
            (
                ScoreManager.instance.score
            );
        }

        alreadyScored = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        alreadyScored = false;
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