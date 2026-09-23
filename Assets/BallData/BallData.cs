using UnityEngine;

[CreateAssetMenu(
    fileName = "NewBallData",
    menuName = "New BallData/Ball Data"
)]
public class BallData : ScriptableObject
{
    public string BallName;

    [Header("Visual")]
    public Material BallMaterial;

    [Header("Physics")]
    public float Mass = 1f;
    public float LinearDamping = 0f;
    public float AngularDamping = 0.05f;

    public PhysicsMaterial PhysicsMaterial;

    [Header("Bounce")]
    public float WallSpeedMultiplier = 1.2f;
    public float GroundSlowMultiplier = 0.8f;
    public float MaxSpeed = 15f;
    public float MinSpeed = 1f;
    public float ExtraBounceForce = 2f;
}