using UnityEngine;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private Transform _pathPointsParent;

    private Transform[] _pathPoints;
    private int _nextPointIndex = 0;
    private Transform _targetPoint;

    private void Start()
    {
        _pathPoints = new Transform[_pathPointsParent.childCount];

        for (int i = 0; i < _pathPointsParent.childCount; i++)
        {
            _pathPoints[i] = _pathPointsParent.GetChild(i);
        }

        if (_pathPoints.Length > 0)
        {
            _targetPoint = _pathPoints[_nextPointIndex];
        }
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _targetPoint.position, _movementSpeed * Time.deltaTime);

        if (transform.position == _targetPoint.position)
        {
            SetNextTargetPoint();
        }
    }

    private void SetNextTargetPoint()
    {
        _nextPointIndex++;

        if (_nextPointIndex == _pathPoints.Length)
        {
            _nextPointIndex = 0;
        }

        _targetPoint = _pathPoints[_nextPointIndex];
    }
}
