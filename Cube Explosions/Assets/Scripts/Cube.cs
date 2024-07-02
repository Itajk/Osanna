using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private int _minSpawnAmount;
    [SerializeField] private int _maxSpawnAmount;

    public event Action CubeClicked;

    public int RandomSpawnAmount => UnityEngine.Random.Range(_minSpawnAmount, _maxSpawnAmount + 1);

    public string GetIdentifiers()
    {
        return $"{name}[{GetInstanceID()}]";
    }

    private void OnMouseUpAsButton()
    {
        CubeClicked?.Invoke();

        Destroy(gameObject);
    }
}
