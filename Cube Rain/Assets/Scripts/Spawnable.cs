using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody), typeof(Collider))]
public class Spawnable : MonoBehaviour
{
    [SerializeField] private float _minCollisionLifespan;
    [SerializeField] private float _maxCollisionLifespan;
    [SerializeField] private float _emissionIntensity;

    private Renderer _renderer;
    private Rigidbody _rigidbody;
    private Spawner _spawner;
    private Coroutine _lifespanCoroutine;
    private bool _hasCollided = false;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<Platform>(out Platform _) && _hasCollided == false)
        {
            _hasCollided = true;

            _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
            _renderer.material.SetColor("_EmissionColor", _renderer.material.color * _emissionIntensity);

            if (_lifespanCoroutine == null)
            {
                _lifespanCoroutine = StartCoroutine(Doom());
            }
        }
    }

    public void SetSpawner(Spawner spawner)
    {
        if (_spawner == null)
        {
            _spawner = spawner;
        }
    }

    public void Initialize(Vector3 spawnPosition)
    {
        gameObject.transform.position = spawnPosition;
        _rigidbody.velocity = Vector3.zero;
        _hasCollided = false;
        _lifespanCoroutine = null;
        _renderer.material.SetColor("_EmissionColor", Color.white * _emissionIntensity);
    }

    private IEnumerator Doom()
    {
        WaitForSeconds wait = new WaitForSeconds(UnityEngine.Random.Range(_minCollisionLifespan, _maxCollisionLifespan));

        yield return wait;

        if (_spawner != null)
        {
            _spawner.ReturnToPool(this);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
