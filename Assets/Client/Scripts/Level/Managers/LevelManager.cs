using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private BirdsSpawner _birdsSpawner;
    [SerializeField] private BirdsLauncher _birdsLauncher;
    [SerializeField] private GameObject _floatingTextPrefab;

    [Header("LevelManagerConfig")]
    [SerializeField] private LevelManagerConfig _config;

    private int _totalUnusedBirdsOnLevel = -1;
    private int _totalPointsOnLevel = 0;
    private int _totalEnemiesOnLevel = 0;
    private bool _levelIsCompleted = false;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }
    private void OnEnable()
    {
        LevelEventManager.OnActiveBirdDisabled.AddListener(ActivateBirdsLauncher);
        LevelEventManager.OnAllBirdsOnLevelSpawned.AddListener(SetTotalUnusedBirdsOnLevel);
        LevelEventManager.OnObstacleDestroyed.AddListener(AddPointsOnObstacleDestroyed);
        LevelEventManager.OnEnemySpawned.AddListener(AddEnemy);
        LevelEventManager.OnEnemyDestroyed.AddListener(OnEnemyDestroyed);
    }
    private void ActivateBirdsLauncher()
    {
        StartCoroutine(ActivateBirdsLauncherCoroutine());
    }
    private void SetTotalUnusedBirdsOnLevel(int amount)
    {
        _totalUnusedBirdsOnLevel = amount;
    }
    private IEnumerator ActivateBirdsLauncherCoroutine()
    {
        yield return new WaitForSeconds(_config.CooldownAfterBirdsDisable);
        _totalUnusedBirdsOnLevel--;
        if (!_levelIsCompleted)
        {
            if (_totalUnusedBirdsOnLevel <= 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
            else
            {
                _birdsLauncher.ActivateLauncher();
            }
        }
    }
    private void AddPointsOnObstacleDestroyed(DestroyableObstacle.ObstacleType type, Vector3 pos)
    {
        switch (type)
        {
            case DestroyableObstacle.ObstacleType.Default:
                _totalPointsOnLevel += _config.PointsForDefaultObstacle;
                SpawnFloatingTextForPoints(_config.PointsForDefaultObstacle, pos, _config.ColorForDestroyedDefaultObstacle);
                break;
            case DestroyableObstacle.ObstacleType.Fragile:
                _totalPointsOnLevel += _config.PointsForFragileObstacle;
                SpawnFloatingTextForPoints(_config.PointsForFragileObstacle, pos, _config.ColorForDestroyedFragileObstacle);
                break;
        }
        LevelEventManager.SendScoreUpdated(_totalPointsOnLevel);
    }
    private void AddEnemy()
    {
        _totalEnemiesOnLevel++;
    }
    private void OnEnemyDestroyed(Vector3 pos)
    {
        _totalEnemiesOnLevel--;
        _totalPointsOnLevel += _config.PointsForEnemy;
        SpawnFloatingTextForPoints(_config.PointsForEnemy, pos, Color.green);
        LevelEventManager.SendScoreUpdated(_totalPointsOnLevel);

        if (_totalEnemiesOnLevel <= 0)
        {
            CompleteLevelAndShowResults();
        }
    }
    private void CompleteLevelAndShowResults()
    {
        _levelIsCompleted = true;
        Debug.Log("Level is completed!");

        if (CameraManager.Singleton != null)
        {
            CameraManager.Singleton.SetFollowTarget(_birdsLauncher.transform);
        }
        else
        {
            Debug.Log("CameraManager is not created!");
        }
        List<GameObject> spawnedBirds = _birdsSpawner.GetAllSpawnedBirds();
        foreach (var bird in spawnedBirds)
        {
            if (!bird.GetComponent<Bird>()._disabled)
            {
                switch (bird.GetComponent<Bird>()._type)
                {
                    case Bird.BirdType.Default:
                        SpawnFloatingTextForPoints(_config.PointsForUnusedDefaultBird, bird.transform.position, _config.ColorForUnusedDefaultBird);
                        _totalPointsOnLevel += _config.PointsForUnusedDefaultBird;
                        break;
                    case Bird.BirdType.Splitting:
                        SpawnFloatingTextForPoints(_config.PointsForUnusedSplittingBird, bird.transform.position, _config.ColorForUnusedSplittingBird);
                        _totalPointsOnLevel += _config.PointsForUnusedSplittingBird;
                        break;
                    case Bird.BirdType.Accelerating:
                        SpawnFloatingTextForPoints(_config.PointsForUnusedAcceleratingBird, bird.transform.position, _config.ColorForUnusedAcceleratingBird);
                        _totalPointsOnLevel += _config.PointsForUnusedAcceleratingBird;
                        break;
                }
            }
        }
        LevelEventManager.SendScoreUpdated(_totalPointsOnLevel);
        LevelEventManager.SendLevelCompeted();
    }
    private void SpawnFloatingTextForPoints(int points, Vector3 pos, Color color)
    {
        if (_floatingTextPrefab != null)
        {
            GameObject floatingText = Instantiate(_floatingTextPrefab, pos, Quaternion.identity);
            floatingText.GetComponentInChildren<TextMesh>().color = color;
            floatingText.GetComponentInChildren<TextMesh>().text = points.ToString();
            if (AudioManager.Singleton != null)
            {
                AudioManager.Singleton.Play("Points_Added");
            }
            else
            {
                Debug.Log("Audio manager is not created!");
            }
            Destroy(floatingText, _config.FloatingTextDestroyTime);
        }
    } 
}
