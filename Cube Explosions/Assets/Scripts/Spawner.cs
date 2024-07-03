using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Cube))]
public class Spawner : MonoBehaviour
{
    [SerializeField] private int _minSpawnAmount;
    [SerializeField] private int _maxSpawnAmount;

    private Cube _cube;

    public event Action<List<Cube>> CubesSpawned;

    public int RandomSpawnAmount => UnityEngine.Random.Range(_minSpawnAmount, _maxSpawnAmount + 1);

    private void Awake()
    {
        _cube = GetComponent<Cube>();
    }

    private void OnEnable()
    {
        _cube.CubeClicked += OnCubeClicked;
    }

    private void OnDisable()
    {
        _cube.CubeClicked -= OnCubeClicked;
    }

    private void OnCubeClicked()
    {
        if (_cube.RollSpawnChance())
        {
            SpawnCubes();
        }
    }

    private void SpawnCubes()
    {
        List<Cube> spawnedCubes;
        Cube spawnedCube;

        int spawnAmount;

        float spawnCircleRotationRadians;

        spawnedCubes = new();
        spawnAmount = RandomSpawnAmount;

        spawnCircleRotationRadians = GetSpawnCircleRotationRadians();

        for (int i = 0; i < spawnAmount; i++)
        {
            spawnedCube = Instantiate(_cube, GetSpawnPosition(i, spawnAmount, spawnCircleRotationRadians), GetSpawnRotation(i, spawnAmount, spawnCircleRotationRadians));
            spawnedCube.Initialize();
            spawnedCubes.Add(spawnedCube);
        }

        CubesSpawned?.Invoke(spawnedCubes);
    }

    private float GetSpawnCircleRotationRadians()
    {
        float fullCircleDegrees = 360;

        return UnityEngine.Random.Range(0, fullCircleDegrees) * Mathf.Deg2Rad;
    }

    private Vector3 GetSpawnPosition(int spawnCounter, int totalSpawns, float spawnCircleRotationRadians)
    {
        float spawnCircleRadius;

        int scaleHalver = 2;
        int fullAngleMultiplier = 2;

        float angleRadians;
        float positionX;
        float positionY;
        float positionZ;

        spawnCircleRadius = gameObject.transform.localScale.y / scaleHalver;

        angleRadians = spawnCounter * Mathf.PI * fullAngleMultiplier / totalSpawns + spawnCircleRotationRadians;
        positionX = Mathf.Cos(angleRadians) * spawnCircleRadius;
        positionY = Vector3.up.y * gameObject.transform.localScale.y / scaleHalver;
        positionZ = Mathf.Sin(angleRadians) * spawnCircleRadius;

        return gameObject.transform.position + new Vector3(positionX, positionY, positionZ);
    }

    private Quaternion GetSpawnRotation(int spawnCounter, int totalSpawns, float spawnCircleRotationRadians)
    {
        int fullAngleMultiplier = 2;

        float angleRadians;
        float angleDegrees;

        angleRadians = spawnCounter * Mathf.PI * fullAngleMultiplier / totalSpawns + spawnCircleRotationRadians;
        angleDegrees = angleRadians * Mathf.Rad2Deg;

        return Quaternion.Euler(0, angleDegrees, 0);
    }
}
