using System.Collections;
using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private Transform _pathPointsParent;
    [SerializeField] private float _movementSpeedPerSecond;
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
        float maxStepPerUpdate = _movementSpeedPerSecond * _movementUpdateFrequency;

        while (enabled)
        {
            while ((transform.position - _targetPoint.position).sqrMagnitude > 0)
            {
                transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, maxStepPerUpdate);

                yield return wait;
            }

            _targetPoint = _pathPoints[++_nextPointIndex % _pathPoints.Length];
        }
    }
}
