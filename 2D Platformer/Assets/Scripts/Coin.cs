using System;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public event Action<Coin> PickedUp;

    public void Initialize(Vector2 position)
    {
        transform.position = position;
    }

    public void PickUp()
    {
        PickedUp?.Invoke(this);
    }
}
