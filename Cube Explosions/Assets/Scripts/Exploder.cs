using System.Collections.Generic;
using UnityEngine;
using System.Linq;

[RequireComponent(typeof(Spawner))]
public class Exploder : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce;
    [SerializeField] private float _baseExplosionRadius;

    private Spawner _spawner;
    private List<Cube> _affectableCubes;

    private void Awake()
    {
        _spawner = GetComponent<Spawner>();
        _affectableCubes = new();
    }

    private void OnEnable()
    {
        _spawner.CubesSpawned += OnCubesSpawned;
    }

    private void OnDisable()
    {
        _spawner.CubesSpawned -= OnCubesSpawned;
    }

    private void OnDestroy()
    {
        Explode();
    }

    private void OnCubesSpawned(List<Cube> spawnedCubes)
    {
        _affectableCubes = spawnedCubes;
    }

    private void Explode()
    {
        float explosionForce;
        float explosionRadius;
        List<Cube> affectedCubes;

        Vector3 explosionDirection;

        if (_affectableCubes.Count > 0)
        {
            foreach (Cube cube in _affectableCubes)
            {
                explosionDirection = Vector3.Normalize(cube.transform.position - gameObject.transform.position) * _baseExplosionForce;

                cube.Rigidbody.AddForce(explosionDirection, ForceMode.Impulse);
            }
        }
        else
        {
            explosionForce = _baseExplosionForce / gameObject.transform.localScale.x;
            explosionRadius = _baseExplosionRadius / gameObject.transform.localScale.x;

            affectedCubes = Physics.OverlapSphere(gameObject.transform.position, explosionRadius).
                Where(collider => collider.gameObject.TryGetComponent<Cube>(out Cube _)).
                Select(collider => collider.gameObject.GetComponent<Cube>()).ToList();

            foreach (Cube cube in affectedCubes)
            {
                explosionDirection = Vector3.Normalize(cube.Rigidbody.transform.position - gameObject.transform.position)
                    * (_baseExplosionForce + explosionForce / Vector3.Distance(cube.Rigidbody.transform.position, gameObject.transform.position));

                cube.Rigidbody.AddForce(explosionDirection, ForceMode.Impulse);
            }
        }
    }
}

