using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Exploder : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private List<Cube> _affectableCubes;

    public string GetIdentifiers()
    {
        return $"{name}[{GetInstanceID()}]";
    }

    private void OnEnable()
    {
        Debug.Log($"{GetIdentifiers()} OnEnable");

        _spawner.CubesSpawned += OnCubesSpawned;
    }

    private void OnDisable()
    {
        Debug.Log($"{GetIdentifiers()} OnDisable");

        _spawner.CubesSpawned -= OnCubesSpawned;
    }

    private void OnCubesSpawned(List<Cube> spawnedCubes)
    {
        Debug.Log($"{GetIdentifiers()} OnCubesSpawned");

        _affectableCubes = spawnedCubes;

        Explode();
    }

    private void Explode()
    {
        List<Rigidbody> affectedRigidbodies;

        Debug.Log($"{GetIdentifiers()} Explode");

        affectedRigidbodies = GetHitColliders().Where(collider => _affectableCubes.Contains(collider.gameObject.GetComponent<Cube>())).
            Select(collider => collider.attachedRigidbody).ToList();

        foreach (Rigidbody rigidbody in affectedRigidbodies)
        {
            rigidbody.AddExplosionForce(_explosionForce, gameObject.transform.position, _explosionRadius);

            Debug.Log($"{rigidbody.GetComponent<Cube>().GetIdentifiers()} affected");
            Debug.DrawLine(gameObject.transform.position, rigidbody.transform.position, Color.red, 2.5f);
        }
    }

    private Collider[] GetHitColliders()
    {
        Collider[] hitColliders;

        hitColliders = Physics.OverlapSphere(gameObject.transform.position, _explosionRadius);

        return hitColliders;
    }
}
