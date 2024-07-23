using System.Collections;
using UnityEngine;


public class Enemy : MonoBehaviour
{
    [SerializeField] private Mover _mover;
    [SerializeField] private EnemyCollisionResolver _collisionResolver;
    [SerializeField] private Animator _animator;

    private bool _isTurning = false;

    private void Start()
    {
        StartCoroutine(Patrolling());
    }

    private IEnumerator Patrolling()
    {
        WaitForSeconds wait = new WaitForSeconds(Time.fixedDeltaTime);

        while (enabled)
        {
            _mover.Move(transform.right.x);

            if ((_collisionResolver.IsOnEdge || _collisionResolver.IsOnDeadEnd) && _isTurning == false)
            {
                _isTurning = true;
                transform.forward = new Vector3(0, 0, -transform.forward.z);
                StartCoroutine(Turning());
            }

            _animator.SetBool(nameof(_mover.IsMoving), _mover.IsMoving);

            yield return wait;
        }
    }

    private IEnumerator Turning()
    {
        WaitForSeconds wait = new WaitForSeconds(1f);

        yield return wait;

        _isTurning = false;
    }
}
