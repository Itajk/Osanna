using UnityEngine;

[RequireComponent(typeof(Collider), typeof(Rigidbody))]
public class Target : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Rigidbody>().useGravity = false;
        GetComponent<Rigidbody>().isKinematic = false;
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Enemy enemy))
        {
            enemy.ReturnToPool();
        }
    }
}