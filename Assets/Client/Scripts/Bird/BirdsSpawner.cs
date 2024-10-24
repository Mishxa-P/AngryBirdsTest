using System.Collections.Generic;
using UnityEngine;

public class BirdsSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _floatingTextPrefab;
    [SerializeField] private Transform _firstBirdSpawnPoint;
    [SerializeField] private float _xOffset = 1.25f;
    [SerializeField] private List<GameObject> _birdsTypes;

    private List<GameObject> _spawnedBirds = new List<GameObject>();
    private int _acitveBirdIndex = -1;

    private void Awake()
    {
        for (int i = 0; i < _birdsTypes.Count; i++)
        {
            Vector3 spawnPoint = _firstBirdSpawnPoint.position + new Vector3(-_xOffset * i, 0, 0);
            _spawnedBirds.Add(Instantiate(_birdsTypes[i], spawnPoint, Quaternion.identity));
        }
    }
    private void Start()
    {
        LevelEventManager.SendAllBirdsOnLevelSpawned(_spawnedBirds.Count);
    }
    public GameObject GetNextBird()
    {
        if (_acitveBirdIndex < _spawnedBirds.Count - 1)
        {
            _acitveBirdIndex++;
            return _spawnedBirds[_acitveBirdIndex];
        }
        else
        {
            return null;
        }
    }
    public List<GameObject> GetAllSpawnedBirds()
    {
        return _spawnedBirds;
    }
}
