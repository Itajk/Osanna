using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class CoinCollector : MonoBehaviour
{
    public event Action CoinPickedUp;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            coin.PickUp();
            CoinPickedUp?.Invoke();
        }
    }
}
