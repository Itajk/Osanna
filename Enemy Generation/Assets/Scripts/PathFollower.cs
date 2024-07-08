using System.Collections;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Transform _pathPointsParent;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _movementUpdateFrequency;

    private Transform[] _pathPoints;
    private Transform _targetPoint;
    private int _nextPointIndex = 0;

    private void Awake()
    {
        _pathPoints = new Transform[_pathPointsParent.childCount];

        for (int i = 0; i < _pathPoints.Length; i++)
        {
            _pathPoints[i] = _pathPointsParent.GetChild(i);
        }

        if (_pathPoints.Length > 0)
        {
            _targetPoint = _pathPoints[_nextPointIndex];
        }
    }

    private void Start()
    {
        StartCoroutine(Moving());
    }

    private IEnumerator Moving()
    {
        WaitForSeconds wait = new WaitForSeconds(_movementUpdateFrequency);
        float closeEnoughDistance = _movementSpeed * _movementUpdateFrequency;

        while (enabled)
        {
            while ((transform.position - _targetPoint.position).sqrMagnitude > closeEnoughDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _movementSpeed);

                yield return wait;
            }

            _targetPoint = _pathPoints[++_nextPointIndex % _pathPoints.Length];
        }
    }
}
