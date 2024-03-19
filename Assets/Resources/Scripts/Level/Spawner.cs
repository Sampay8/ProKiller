using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private SpawnPoint _playerSpawn;
    [SerializeField] private List<Vector3> _characterSpawnPositions = new List<Vector3>(); 
    [SerializeField] private List<Character> _characters = new List<Character>();
    [SerializeField] private List<Character> _pool;

    public List<Character> Pool => _pool;

    private System.Random Random = new System.Random();
    private int _spawnCount = 3;
    private int _enemySpawnCount = 3;
    private Coroutine _poolRoutine;
    private WaitForEndOfFrame _waitForEndOfFrame = new WaitForEndOfFrame();
    private CharacterWayPoint _wayPoint;

    private void OnEnable()
    {
        FindWaipoints();
        CreatePool();
    }

    private void OnDisable()
    {
        ClearPool();
    }

    private void FindWaipoints()
    {
        _wayPoint = FindObjectOfType<CharacterWayPoint>();
    }

    private void ClearPool()
    {
        if (_pool != null)
            foreach (var character in _pool)
                Destroy(character.gameObject);
    }

    private void CreatePool()
    {
        _pool = new List<Character>();
        for (int i = 0; i < _spawnCount; i++)
        {
            var character = Instantiate(_characters[Random.Next(0, _characters.Count)], this.transform);
            _pool.Add(character);
            character.Init(_wayPoint, _characterSpawnPositions);
        }
    }
}
