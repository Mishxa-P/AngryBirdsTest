using UnityEngine;

[CreateAssetMenu(menuName = "Configs/BirdsLauncherConfig", fileName = "BirdsLauncherConfig")]
public class BirdsLauncherConfig : ScriptableObject
{
    [field: SerializeField] public float FirstTimeActivationDelay { get; private set; }
    [field: SerializeField] public float Power { get; private set; }
    [field: SerializeField] public float MaxPullRadius { get; private set; }                       
    [field: SerializeField] public int LinePositionsCount { get; private set; }
    [field: SerializeField] public float LineDeltaTime { get; private set; }
}
