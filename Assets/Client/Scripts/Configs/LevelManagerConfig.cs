using UnityEngine;

[CreateAssetMenu(menuName = "Configs/LevelManagerConfig", fileName = "LevelManagerConfig")]
public class LevelManagerConfig : ScriptableObject
{
    [field: SerializeField] public float CooldownAfterBirdsDisable { get; private set; }
    [field: SerializeField] public int PointsForDefaultObstacle { get; private set; }
    [field: SerializeField] public int PointsForFragileObstacle { get; private set; }
    [field: SerializeField] public int PointsForEnemy { get; private set; }
    [field: SerializeField] public int PointsForUnusedDefaultBird { get; private set; }
    [field: SerializeField] public int PointsForUnusedSplittingBird { get; private set; }
    [field: SerializeField] public int PointsForUnusedAcceleratingBird { get; private set; }
    [field: SerializeField] public float FloatingTextDestroyTime { get; private set; }
    [field: SerializeField] public Color ColorForUnusedDefaultBird { get; private set; }
    [field: SerializeField] public Color ColorForUnusedSplittingBird { get; private set; }
    [field: SerializeField] public Color ColorForUnusedAcceleratingBird { get; private set; }
    [field: SerializeField] public Color ColorForDestroyedDefaultObstacle { get; private set; }
    [field: SerializeField] public Color ColorForDestroyedFragileObstacle { get; private set; }
    [field: SerializeField] public Color ColorForDestroyedEnemy { get; private set; }
}
