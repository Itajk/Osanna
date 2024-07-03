using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _explosionForce;

    private List<Cube> _affectableCubes;

    private void OnEnable()
    {
        _spawner.CubesSpawned += OnCubesSpawned;
    }

    private void OnDisable()
    {
        _spawner.CubesSpawned -= OnCubesSpawned;
    }

    private void OnCubesSpawned(List<Cube> spawnedCubes)
    {
        _affectableCubes = spawnedCubes;

        Explode();
    }

    private void Explode()
    {
        List<Rigidbody> affectedRigidbodies;

        Vector3 explosionDirection;

        affectedRigidbodies = _affectableCubes.Select(cube => cube.gameObject.GetComponent<Rigidbody>()).Where(rigidbody => rigidbody != null).ToList();

        foreach (Rigidbody rigidbody in affectedRigidbodies)
        {
            if (rigidbody != null)
            {
                explosionDirection = Vector3.Normalize(rigidbody.transform.position - gameObject.transform.position) * _explosionForce;
                rigidbody.AddForce(explosionDirection, ForceMode.Impulse);
            }
        }
    }
}
