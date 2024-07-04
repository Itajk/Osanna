using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Returner : MonoBehaviour
{
    private Collider _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider>();
        _collider.isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent<Spawnable>(out Spawnable spawnable))
        {
            spawnable.ReturnToPool();
        }
    }
}
