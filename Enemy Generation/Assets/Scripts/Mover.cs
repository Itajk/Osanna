using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Mover : MonoBehaviour
{
    [SerializeField] private List<Transform> _movePoints;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _movementUpdateFrequency;

    private int _movePointCounter = 0;
    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.useGravity = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    public void AddMovePoint(Transform movePoint)
    {
        _movePoints.Add(movePoint);
    }

    public void StartMoving()
    {
        MoveTowards(_movePoints[_movePointCounter]);
    }

    private void MoveTowards(Transform point)
    {
        StartCoroutine(KeepMovingTowards(point));
    }

    private IEnumerator KeepMovingTowards(Transform point)
    {
        WaitForSeconds wait = new WaitForSeconds(_movementUpdateFrequency);
        int fullCircleDegrees = 360;
        float closeEnoughDistance;

        closeEnoughDistance = _movementSpeed * _movementUpdateFrequency;

        while (Vector3.Distance(transform.position, point.position) > closeEnoughDistance)
        {
            _rigidbody.velocity = Vector3.RotateTowards(transform.forward, point.position - transform.position, Mathf.Deg2Rad * fullCircleDegrees, 0) * _movementSpeed;

            yield return wait;
        }

        _movePointCounter++;

        if (_movePointCounter >= _movePoints.Count)
        {
            _movePointCounter = 0;
        }

        MoveTowards(_movePoints[_movePointCounter]);
    }
}
