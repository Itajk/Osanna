using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using System.Linq;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] private ClickableObject _clickableObject;
    [SerializeField] private int _minSpawnAmount;
    [SerializeField] private int _maxSpawnAmount;

    [SerializeField] private float _spawnChancePercent;
    [SerializeField] private float _spawnChanceDiviser;

    public event Action<GameObject> ObjectSpawned;
    public event Action ObjectsSpawned;

    private void OnEnable()
    {
        _clickableObject.ObjectClicked += SpawnObjects;
    }

    private void OnDisable()
    {
        _clickableObject.ObjectClicked -= SpawnObjects;
    }

    private void SpawnObjects()
    {
        int minPercent = 0;
        int maxPercent = 100;
        int rollResult;

        int spawnAmount;

        GameObject spawnedObject;
        Vector3 spawnPosition;
        Quaternion zeroRotation = new Quaternion(0f, 0f, 0f, 0f);
        float circleDegrees = 360;

        rollResult = UnityEngine.Random.Range(minPercent, maxPercent);

        if (rollResult < _spawnChancePercent)
        {
            spawnAmount = UnityEngine.Random.Range(_minSpawnAmount, _maxSpawnAmount + 1);

            for (int i = 0; i < spawnAmount; i++)
            {
                spawnPosition = CalculateSpawnPosition();
                spawnedObject = Instantiate(gameObject, spawnPosition, zeroRotation);
                transform.RotateAround(transform.position, Vector3.up, circleDegrees / spawnAmount);

                spawnedObject.GetComponent<Scaler>().Rescale();
                spawnedObject.GetComponent<ColorChanger>().Recolor();
                spawnedObject.GetComponent<Spawner>()._spawnChancePercent /= _spawnChanceDiviser;

                ObjectSpawned?.Invoke(spawnedObject);
            }

            ObjectsSpawned?.Invoke();
        }
    }

    private Vector3 CalculateSpawnPosition()
    {
        Vector3 spawnedObjectPosition;

        float distanceToEdge;
        float distanceMultiplier = 2f;

        float halfScaleDiviser = 2f;

        float powerOfTwo = 2f;

        float distanceToTopSide;

        Vector3 positionAtEdge;

        distanceToEdge = Mathf.Sqrt(Mathf.Pow(transform.localScale.x / halfScaleDiviser, powerOfTwo)
            + Mathf.Pow(transform.localScale.z / halfScaleDiviser, powerOfTwo));
        positionAtEdge = transform.position + transform.forward * distanceToEdge * distanceMultiplier;
        distanceToTopSide = transform.position.y + transform.localScale.y / halfScaleDiviser;
        spawnedObjectPosition = new Vector3(positionAtEdge.x, distanceToTopSide, positionAtEdge.z);

        return spawnedObjectPosition;
    }
}
