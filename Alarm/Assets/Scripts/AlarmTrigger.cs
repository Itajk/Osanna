using System;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class AlarmTrigger : MonoBehaviour
{
    public event Action ThiefEntered;
    public event Action ThiefLeft;

    private void Awake()
    {
        GetComponent<Collider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PathFollower _))
        {
            ThiefEntered?.Invoke();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PathFollower _))
        {
            ThiefLeft?.Invoke();
        }
    }
}
