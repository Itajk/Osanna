using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

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

        Color(cube);
    }

    private void Color(Cube cube)
    {
        Debug.Log($"{GetIdentifiers()} ChangeColor");

        cube.GetComponent<Renderer>().material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        Color color;
        float redComponent;
        float greenComponent;
        float blueComponent;

        redComponent = UnityEngine.Random.Range(0.000f, 1.000f);
        greenComponent = UnityEngine.Random.Range(0.000f, 1.000f);
        blueComponent = UnityEngine.Random.Range(0.000f, 1.000f);

        color = new Color(redComponent, greenComponent, blueComponent);

        return color;
    }
}
