using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _scaleDiviser;

    public string GetIdentifiers()
    {
        return $"{name}[{GetInstanceID()}]";
    }

    private void OnEnable()
    {
        Debug.Log($"{GetIdentifiers()} OnEnable");

        _spawner.CubeSpawned += OnCubeSpawned;
    }

    private void OnDisable()
    {
        Debug.Log($"{GetIdentifiers()} OnDisable");

        _spawner.CubeSpawned -= OnCubeSpawned;
    }

    private void OnCubeSpawned(Cube cube)
    {
        Debug.Log($"{GetIdentifiers()} OnCubeSpawned");

        DivideScale(cube);
    }

    private void DivideScale(Cube cube)
    {
        Debug.Log($"{GetIdentifiers()} DivideScale");

        cube.transform.localScale /= _scaleDiviser;
    }
}
