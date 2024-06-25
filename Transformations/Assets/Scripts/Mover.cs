using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private Vector3 _movementDirection;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Space _space;

    private void Update()
    {
        transform.Translate(_movementDirection.normalized * _movementSpeed, _space);
    }
}
