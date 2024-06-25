using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class BackAndForthMover : MonoBehaviour
{
    [SerializeField] private Vector3 _movementDirection;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Space _space;

    private Vector3 _centralPosition;
    private Vector3 _newPosition;

    [SerializeField] private int _maxMoveDistance;
    private float _moveDistance;

    private int _oppositeDirectionMultiplier = -1;

    private void Start()
    {
        _centralPosition = transform.position;
    }

    private void OnValidate()
    {
        _centralPosition = transform.position;
    }

    private void Update()
    {
        _newPosition = transform.position;
        _moveDistance = Math.Abs(Vector3.Distance(_centralPosition, _newPosition));

        if (_moveDistance >= _maxMoveDistance)
        {
            _movementDirection *= _oppositeDirectionMultiplier;
        }

        transform.Translate(_movementDirection.normalized * _movementSpeed, _space);
    }
}
