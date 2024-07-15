using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private Transform[] _spawnPathsParents;

    private List<CoinSpawnPath> _spawnPaths;

    private ObjectPool<Coin> _pool;
    private int _poolCapacity = 250;
    private int _poolMaxSize = 500;

    private void Awake()
    {
        _pool = new ObjectPool<Coin>(
            createFunc: () => CreateCoin(),
            actionOnGet: (coin) => Respawn(coin),
            actionOnRelease: (coin) => coin.gameObject.SetActive(false),
            actionOnDestroy: (coin) => Destroy(coin),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );

        _spawnPaths = new();

        foreach(Transform spawnPathParent in _spawnPathsParents)
        {
            if (spawnPathParent.TryGetComponent(out CoinSpawnPath _spawnPath))
            {
                _spawnPaths.Add(_spawnPath);
            }
        }
    }

    private void Start()
    {
        foreach (CoinSpawnPath spawnPath in _spawnPaths)
        {
            while (spawnPath.IsEndReached == false)
            {
                spawnPath.PlaceCoin(_pool.Get());
            }

            spawnPath.PlaceCoin(_pool.Get());
        }
    }

    public void Spawn()
    {
        _pool.Get();
    }

    private void OnCoinDisappeared(Coin coin)
    {
        _pool.Release(coin);
    }

    private Coin CreateCoin()
    {
        Coin coin = Instantiate(_coinPrefab);
        coin.Disappeared += OnCoinDisappeared;

        return coin;
    }

    private void Respawn(Coin coin)
    {
        coin.gameObject.SetActive(true);
    }
}
