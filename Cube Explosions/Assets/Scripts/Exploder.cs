using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Spawner))]
public class Exploder : MonoBehaviour
{
    [SerializeField] private float _baseExplosionForce;

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
        Vector3 explosionDirection;

        if (_affectableCubes.Count > 0)
        {
            foreach (Cube cube in _affectableCubes)
            {
                explosionDirection = Vector3.Normalize(cube.transform.position - gameObject.transform.position) * _baseExplosionForce;

                cube.Rigidbody.AddForce(explosionDirection, ForceMode.Impulse);
            }
        }
    }
}
