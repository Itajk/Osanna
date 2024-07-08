using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _spawnPointsParent;
    [SerializeField] private float _spawnRate;

    private List<SpawnPoint> _spawnPoints;

    private void Awake()
    {
        _spawnPoints = new List<SpawnPoint>();

        for (int i = 0; i < _spawnPointsParent.childCount; i++)
        {
            if (_spawnPointsParent.GetChild(i).TryGetComponent(out SpawnPoint spawnPoint))
            {
                _spawnPoints.Add(spawnPoint);
            }
        }
    }

    private void Start()
    {
        StartCoroutine(Spawning());
    }

    private IEnumerator Spawning()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnRate);

        while (enabled)
        {
            _spawnPoints[Random.Range(0, _spawnPoints.Count)].Spawn();

            yield return wait;
        }
    }
}

