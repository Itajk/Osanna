using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _movementSpeedPerSecond;
    [SerializeField] private Transform _pathPointsParent;

    private float _movementUpdateFrequency = 0.05f;
    private Transform[] _pathPoints;
    private int _nextPointIndex = 0;
    private Transform _targetPoint;

    private void Awake()
    {
        _pathPoints = new Transform[_pathPointsParent.childCount];

        for (int i = 0; i < _pathPoints.Length; i++)
        {
            _pathPoints[i] = _pathPointsParent.GetChild(i);
        }
    }

    private void Start()
    {
        if (_pathPoints.Length > 0)
        {
            _targetPoint = _pathPoints[_nextPointIndex];

            StartCoroutine(Moving());
        }
    }

    private IEnumerator Moving()
    {
        WaitForSeconds wait = new WaitForSeconds(_movementUpdateFrequency);

        while (enabled)
        {
            while ((transform.position - _targetPoint.position).sqrMagnitude > 0)
            {
                transform.LookAt(_targetPoint.position);
                transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _movementSpeedPerSecond * _movementUpdateFrequency);

                yield return wait;
            }

            _targetPoint = _pathPoints[++_nextPointIndex % _pathPoints.Length];
        }
    }
}
