using System;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube cube;
    [SerializeField] private float _spawnChancePercent;
    [SerializeField] private float _spawnChanceDiviser;

    [SerializeField] private int _minSpawnAmount;
    [SerializeField] private int _maxSpawnAmount;

    private List<Cube> _spawnedCubes;

    public event Action CubeSpawned;
    public event Action<List<Cube>> CubesSpawned;

    public int RandomSpawnAmount => UnityEngine.Random.Range(_minSpawnAmount, _maxSpawnAmount + 1);

    private void Awake()
    {
        _spawnedCubes = new();
    }

    private void OnEnable()
    {
        cube.CubeClicked += OnCubeClicked;
    }

    private void OnDisable()
    {
        cube.CubeClicked -= OnCubeClicked;
    }

    private void OnCubeClicked()
    {
        if (RollSpawnChance())
        {
            _spawnChancePercent /= _spawnChanceDiviser;
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
            spawnedCube = Instantiate(cube, GetSpawnPosition(i, spawnAmount, spawnCircleRotationRadians), GetSpawnRotation(i, spawnAmount, spawnCircleRotationRadians));
            spawnedCubes.Add(spawnedCube);

            spawnedCube.SubscribeCubeSpawned(this);
            CubeSpawned?.Invoke();
            spawnedCube.UnsubscribeCubeSpawned(this);
        }

        _spawnedCubes.AddRange(spawnedCubes);

        CubesSpawned?.Invoke(spawnedCubes);
    }

    private float GetSpawnCircleRotationRadians()
    {
        float fullCircleDegrees = 360;

        return UnityEngine.Random.Range(0, fullCircleDegrees) * Mathf.Deg2Rad;
    }

    private Vector3 GetSpawnPosition(int spawnCounter, int totalSpawns, float spawnCircleRotationRadians)
    {
        float spawnCircleRadius = 5f;

        int scaleHalver = 2;
        int fullAngleMultiplier = 2;

        float angleRadians;
        float positionX;
        float positionY;
        float positionZ;

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

    private bool RollSpawnChance()
    {
        float minChancePercent = 0;
        float maxChancePercent = 100;

        return _spawnChancePercent > UnityEngine.Random.Range(minChancePercent, maxChancePercent - 1);
    }
}
