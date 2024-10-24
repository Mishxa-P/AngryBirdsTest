using UnityEngine.Events;

public class LevelEventManager
{
    public static UnityEvent OnActiveBirdDisabled = new UnityEvent();
    public static UnityEvent<int> OnAllBirdsOnLevelSpawned = new UnityEvent<int>();
    public static UnityEvent<DestroyableObstacle.ObstacleType, UnityEngine.Vector3> OnObstacleDestroyed = new UnityEvent<DestroyableObstacle.ObstacleType, UnityEngine.Vector3>();
    public static UnityEvent OnEnemySpawned = new UnityEvent();
    public static UnityEvent<UnityEngine.Vector3> OnEnemyDestroyed = new UnityEvent<UnityEngine.Vector3>();
    public static UnityEvent<int> OnScoreUpdated = new UnityEvent<int>();
    public static UnityEvent OnLevelCompleted = new UnityEvent();

    public static void SendActiveBirdDisabled()
    {
        OnActiveBirdDisabled?.Invoke();
    }
    public static void SendAllBirdsOnLevelSpawned(int amount)
    {
        OnAllBirdsOnLevelSpawned?.Invoke(amount);
    }
    public static void SendObstacleDestroyed(DestroyableObstacle.ObstacleType type, UnityEngine.Vector3 pos)
    { 
        OnObstacleDestroyed?.Invoke(type, pos); 
    }
    public static void SendEnemySpawned()
    {
        OnEnemySpawned?.Invoke();
    }
    public static void SendEnemyDestroyed(UnityEngine.Vector3 pos)
    {
        OnEnemyDestroyed?.Invoke(pos);
    }
    public static void SendScoreUpdated(int newScore)
    {
        OnScoreUpdated?.Invoke(newScore);
    }
    public static void SendLevelCompeted()
    {
        OnLevelCompleted?.Invoke();
    }
}
