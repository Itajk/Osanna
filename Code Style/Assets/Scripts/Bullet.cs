using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _lifespan;

    private Rigidbody _rigidbody;
    private WaitForSeconds _wait;

    public event Action<Bullet> Died;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
        _wait = new WaitForSeconds(_lifespan);
    }

    private void OnEnable()
    {
        StartCoroutine(Flying());
    }

    public void Initialize(Vector3 spawnPosition)
    {
        transform.position = spawnPosition;
        _rigidbody.velocity = transform.position.normalized * _speed;
    }

    public void TargetReached()
    {
        Died?.Invoke(this);
    }

    private IEnumerator Flying()
    {
        yield return _wait;

        Died?.Invoke(this);
    }
}
