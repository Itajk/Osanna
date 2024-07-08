using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Mover), typeof(Collider))]
public class Enemy : MonoBehaviour
{
    private Collider _collider;
    private Renderer _renderer;
    private Mover _mover;
    private Spawner _spawner;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        _renderer = GetComponent<Renderer>();
        _mover = GetComponent<Mover>();
    }

    public void SetSpawner(Spawner spawner)
    {
        if (_spawner == null)
        {
            _spawner = spawner;
        }
    }

    public void SetTarget(Transform target)
    {
        _mover.AddMovePoint(target);
    }

    public void Initialize(Vector3 spawnPosition, Color color)
    {
        transform.position = spawnPosition;
        _renderer.material.color = color;
        _mover.StartMoving();
    }

    public void ReturnToPool()
    {
        _spawner.ReturnToPool(this);
    }
}
