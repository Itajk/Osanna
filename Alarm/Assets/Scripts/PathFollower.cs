using UnityEngine;
using System.Collections;

public class PathFollower : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _path;

    private Transform[] _waypoints;
    private int _currentWaypoint = 0;
    private Transform _target;

    private float _updateFrequency = 0.05f;
    private float _closeEnoughDistance = 0.1f;

    private void Awake()
    {
        _waypoints = new Transform[_path.childCount];

        for (int i = 0; i < _waypoints.Length; i++)
        {
            _waypoints[i] = _path.GetChild(i);
        }
    }

    private void Start()
    {
        if (_waypoints.Length > 0)
        {
            _target = _waypoints[_currentWaypoint];

            StartCoroutine(Moving());
        }
    }

    private IEnumerator Moving()
    {
        WaitForSeconds wait = new WaitForSeconds(_updateFrequency);

        while (enabled)
        {
            while ((transform.position - _target.position).sqrMagnitude > _closeEnoughDistance)
            {
                RotateTowardsTarget();
                transform.position = Vector3.MoveTowards(transform.position, _target.position,
                    _speed * _updateFrequency);

                yield return wait;
            }

            _target = _waypoints[++_currentWaypoint % _waypoints.Length];
        }
    }

    private void RotateTowardsTarget()
    {
        Vector2 forwardDirection;
        Vector3 targetDirection3D;
        Vector2 targetDirection;

        forwardDirection = new Vector2(transform.forward.z, transform.forward.x);
        targetDirection3D = (_target.transform.position - transform.position).normalized;
        targetDirection = new Vector2(targetDirection3D.z, targetDirection3D.x);
        transform.Rotate(0, Vector2.SignedAngle(forwardDirection, targetDirection), 0);
    }
}
