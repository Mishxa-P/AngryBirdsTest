using UnityEngine;

[CreateAssetMenu(menuName = "Configs/DestroyableObstacleConfig", fileName = "DestroyableObstacleConfig")]
public class DestroyableObstacleConfig : ScriptableObject
{
    [field: SerializeField] public DestroyableObstacle.ObstacleType Type { get; private set; }
    [field: SerializeField] public float BirdVelMagnitudeRequiredForDestruction { get; private set; }
    [field: SerializeField] public float DefaultBirdVelSlow { get; private set; }
    [field: SerializeField] public float AppropriateBirdVelSlow { get; private set; }
}
