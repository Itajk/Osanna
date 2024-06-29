using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using System;
using Unity.VisualScripting;
using UnityEngine.Rendering.Universal;

public class Exploder : MonoBehaviour
{
    [SerializeField] private float _explosionRadius;
    [SerializeField] private float _explosionForce;

    private List<GameObject> _affectableObjects;
    private List<int> _affectableObjectsIdentifiers;

    private void Start()
    {
        _affectableObjects = new List<GameObject>();
        _affectableObjectsIdentifiers = new List<int>();
    }

    private void OnEnable()
    {
        GetComponent<Spawner>().ObjectSpawned += AddAffectableObject;
        GetComponent<Spawner>().ObjectsSpawned += Explode;
    }

    private void OnDisable()
    {
        GetComponent<Spawner>().ObjectSpawned -= AddAffectableObject;
        GetComponent<Spawner>().ObjectsSpawned -= Explode;
    }

    private void AddAffectableObject(GameObject affectableObject)
    {
        _affectableObjects.Add(affectableObject);
    }

    private void Explode()
    {
        List<Collider> affectedColliders;
        List<Rigidbody> affectedRigidbodies;

        affectedColliders = GetAffectedColliders();
        affectedRigidbodies = affectedColliders.Select(collider => collider.attachedRigidbody).ToList();

        foreach (Rigidbody affectedRigidbody in affectedRigidbodies)
        {
            affectedRigidbody.AddExplosionForce(_explosionForce, transform.position, _explosionRadius);
        }
    }

    private List<Collider> GetAffectedColliders()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, _explosionRadius);
        List<Collider> affectedColliders = new List<Collider>();

        affectedColliders = hitColliders.Where(collider => _affectableObjects.Contains(collider.gameObject)).ToList();

        return affectedColliders;
    }
}
