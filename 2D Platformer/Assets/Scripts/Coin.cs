using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(Animator))]
public class Coin : MonoBehaviour
{
    public event Action<Coin> Disappeared;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            player.AddCoin();

            Disappeared?.Invoke(this);
        }
    }

    public void Initialize(Vector2 spawnPosition)
    {
        transform.position = spawnPosition;
    }
}
