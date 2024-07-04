using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody), typeof(Collider))]
public class Spawnable : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;

    private Collider _collider;
    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Spawner _spawner;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    public void SetSpawner(Spawner spawner)
    {
        if (_spawner == null)
        {
            _spawner = spawner;
        }
    }

    public void Initialize(Vector3 spawnPosition, Color color, Vector3 movementDirection)
    {
        gameObject.transform.position = spawnPosition;
        _renderer.material.color = color;
        _rigidbody.velocity = Vector3.Normalize(movementDirection) * _movementSpeed;
    }

    public void ReturnToPool()
    {
        _spawner.ReturnToPool(this);
    }
}
