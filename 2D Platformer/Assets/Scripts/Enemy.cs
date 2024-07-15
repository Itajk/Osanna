using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider2D), typeof(Rigidbody2D))]
public class Enemy : MonoBehaviour
{
    private const string IsMovingVariableName = "IsMoving";

    [SerializeField] private float _speed;
    [SerializeField] private Transform _path;

    private Transform[] _waypoints;
    private int _currentWaypoint = 0;
    private Transform _target;
    private Animator _animator;

    private float _updateFrequency = 0.02f;
    private float _closeEnoughDistance = 0.1f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();

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

        _animator.SetBool(IsMovingVariableName, true);

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

        _animator.SetBool(IsMovingVariableName, false);
    }

    private void RotateTowardsTarget()
    {
        Vector2 forwardDirection;
        Vector3 targetDirection3D;
        Vector2 targetDirection;

        forwardDirection = transform.right;
        targetDirection3D = (_target.transform.position - transform.position).normalized;
        targetDirection = new Vector2(targetDirection3D.x, 0);
        transform.Rotate(0, Vector2.SignedAngle(forwardDirection, targetDirection), 0);
    }
}
