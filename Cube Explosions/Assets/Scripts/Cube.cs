using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;

    public event Action CubeClicked;
    public event Action SelfSpawned;

    public void SubscribeCubeSpawned(Spawner spawner)
    {
        spawner.CubeSpawned += OnCubeSpawned;
    }

    public void UnsubscribeCubeSpawned(Spawner spawner)
    {
        spawner.CubeSpawned -= OnCubeSpawned;
    }

    private void OnMouseUpAsButton()
    {
        CubeClicked?.Invoke();

        Destroy(gameObject);
    }

    private void OnCubeSpawned()
    {
        SelfSpawned?.Invoke();
    }
}
