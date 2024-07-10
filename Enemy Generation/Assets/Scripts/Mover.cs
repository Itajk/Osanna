using System.Collections;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float _movementSpeedPerSecond;
    [SerializeField] private float _movementUpdateFrequency;

    public void StartMoving(Transform target)
    {
        StartCoroutine(Moving(target));
    }

    private IEnumerator Moving(Transform target)
    {
        WaitForSeconds wait = new WaitForSeconds(_movementUpdateFrequency);
        float maxStepPerUpdate = _movementSpeedPerSecond * _movementUpdateFrequency;

        while ((transform.position - target.position).sqrMagnitude > 0)
        {
            transform.LookAt(target.position);
            transform.position = Vector3.MoveTowards(transform.position, target.position, maxStepPerUpdate);

            yield return wait;
        }
    }
}
