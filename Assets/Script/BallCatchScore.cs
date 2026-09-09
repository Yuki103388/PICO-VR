using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BallCatchScore : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;

    private bool alreadyScored;

    private void Awake()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();

        grabInteractable.selectEntered.AddListener(OnCaught);
        grabInteractable.selectExited.AddListener(OnReleased);
    }

    //private void Update()
    //{
    //    if (Input.GetKeyDown(KeyCode.Space))
    //    {
    //        ScoreManager.instance.AddScore(1);
    //    }
    //}

    private void OnCaught(SelectEnterEventArgs args)
    {
        if (alreadyScored)
            return;

        ScoreManager.instance.AddScore(1);
        alreadyScored = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        alreadyScored = false;
    }
}