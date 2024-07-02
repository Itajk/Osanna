using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.Experimental.AssetDatabaseExperimental.AssetDatabaseCounters;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Cube cube;
    [SerializeField] private float _spawnChancePercent;
    [SerializeField] private float _spawnChanceDiviser;

    private List<Cube> _spawnedCubes;

    public event Action<Cube> CubeSpawned;
    public event Action<List<Cube>> CubesSpawned;

    public string GetIdentifiers()
    {
        return $"{name}[{GetInstanceID()}]";
    }

    private void Awake()
    {
        Debug.Log($"{GetIdentifiers()} Awake");

        _spawnedCubes = new();
    }

    private void OnEnable()
    {
        Debug.Log($"{GetIdentifiers()} OnEnable");

        cube.CubeClicked += OnCubeClicked;
    }

    private void OnDisable()
    {
        Debug.Log($"{GetIdentifiers()} OnDisable");

        cube.CubeClicked -= OnCubeClicked;
    }

    private void OnCubeClicked()
    {
        Debug.Log($"{GetIdentifiers()} OnCubeClicked");

        if (RollSpawnChance())
        {
            DivideSpawnChancePercent();
            SpawnCubes();
        }
    }

    private void SpawnCubes()
    {
        List<Cube> spawnedCubes;
        Cube spawnedCube;

        int spawnAmount;

        float spawnCircleRotationRadians;

        Debug.Log($"{GetIdentifiers()} SpawnCubes");

        spawnedCubes = new();
        spawnAmount = cube.RandomSpawnAmount;

        spawnCircleRotationRadians = GetSpawnCircleRotationRadians();

        for (int i = 0; i < spawnAmount; i++)
        {
            spawnedCube = SpawnCube(GetSpawnPosition(i, spawnAmount, spawnCircleRotationRadians), GetSpawnRotation(i, spawnAmount, spawnCircleRotationRadians));
            spawnedCubes.Add(spawnedCube);
        }

        _spawnedCubes.AddRange(spawnedCubes);

        CubesSpawned?.Invoke(spawnedCubes);
    }

    private Cube SpawnCube(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        Cube spawnedCube;

        Debug.Log($"{GetIdentifiers()} SpawnCube");

        spawnedCube = Instantiate(cube, spawnPosition, spawnRotation);

        CubeSpawned?.Invoke(spawnedCube);

        return spawnedCube;
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
        Vector3 spawnPosition;

        angleRadians = spawnCounter * Mathf.PI * fullAngleMultiplier / totalSpawns + spawnCircleRotationRadians;
        positionX = Mathf.Cos(angleRadians) * spawnCircleRadius;
        positionY = Vector3.up.y * gameObject.transform.localScale.y / scaleHalver;
        positionZ = Mathf.Sin(angleRadians) * spawnCircleRadius;
        spawnPosition = gameObject.transform.position + new Vector3(positionX, positionY, positionZ);

        return spawnPosition;
    }

    private Quaternion GetSpawnRotation(int spawnCounter, int totalSpawns, float spawnCircleRotationRadians)
    {
        int fullAngleMultiplier = 2;

        float angleRadians;
        float angleDegrees;
        Quaternion spawnRotation;

        angleRadians = spawnCounter * Mathf.PI * fullAngleMultiplier / totalSpawns + spawnCircleRotationRadians;
        angleDegrees = angleRadians * Mathf.Rad2Deg;
        spawnRotation = Quaternion.Euler(0, angleDegrees, 0);

        return spawnRotation;
    }

    private bool RollSpawnChance()
    {
        float minChancePercent = 0;
        float maxChancePercent = 100;
        float rolledChance;

        rolledChance = UnityEngine.Random.Range(minChancePercent, maxChancePercent - 1);

        return _spawnChancePercent > rolledChance;
    }

    private void DivideSpawnChancePercent()
    {
        _spawnChancePercent /= _spawnChanceDiviser;
    }
}
