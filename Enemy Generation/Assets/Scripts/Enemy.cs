using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Mover), typeof(Collider))]
public class Enemy : MonoBehaviour
{
    private Renderer _renderer;
    private Mover _mover;

    public event Action<Enemy> Died;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _mover = GetComponent<Mover>();
    }

    public void Initialize(Vector3 spawnPosition, Color color, Transform target)
    {
        transform.position = spawnPosition;
        _renderer.material.color = color;
       _mover.StartMoving(target);
    }

    public void TargetReached()
    {
        Died?.Invoke(this);
    }
}
