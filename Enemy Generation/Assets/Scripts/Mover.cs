using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _movementUpdateFrequency;

    private Coroutine _movingCoroutine;

    private void OnDisable()
    {
        StopCoroutine(_movingCoroutine);
    }

    public void StartMoving(Transform target)
    {
        _movingCoroutine = StartCoroutine(Moving(target));
    }

    private IEnumerator Moving(Transform target)
    {
        WaitForSeconds wait = new WaitForSeconds(_movementUpdateFrequency);
        float closeEnoughDistance = _movementSpeed * _movementUpdateFrequency;

        while ((transform.position - target.position).sqrMagnitude > closeEnoughDistance)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, _movementSpeed);

            yield return wait;
        }
    }
}
