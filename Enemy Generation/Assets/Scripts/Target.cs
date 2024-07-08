using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Mover))]
public class Target : MonoBehaviour
{
    private Mover _mover;
    private Collider _collider;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Enemy>(out Enemy spawnable))
        {
            spawnable.ReturnToPool();
        }
    }

    private void Start()
    {
        _mover.StartMoving();
    }
}