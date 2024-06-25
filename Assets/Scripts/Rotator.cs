using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationDirection;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private Space _space;

    private void Update()
    {
        transform.Rotate(
            _rotationDirection.normalized.x * _rotationSpeed,
            _rotationDirection.normalized.y * _rotationSpeed,
            _rotationDirection.normalized.z * _rotationSpeed, _space);
    }
}
