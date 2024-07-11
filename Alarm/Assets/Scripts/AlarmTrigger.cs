using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AlarmTrigger : MonoBehaviour
{
    public event Action FrontEntered;
    public event Action BackEntered;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PathFollower _))
        {
            if (IsFrontEntered(other))
            {
                FrontEntered?.Invoke();
            }
            else
            {
                BackEntered?.Invoke();
            }
        }
    }

    private bool IsFrontEntered(Collider other)
    {
        float frontalArcDegrees = 90;
        Vector2 forwardDirection;
        Vector3 targetDirection3D;
        Vector2 targetDirection;

        forwardDirection = new Vector2(transform.forward.z, transform.forward.x);
        targetDirection3D = (other.transform.position - transform.position).normalized;
        targetDirection = new Vector2(targetDirection3D.z, targetDirection3D.x);

        return Vector2.Angle(forwardDirection, targetDirection) <= frontalArcDegrees;
    }
}
