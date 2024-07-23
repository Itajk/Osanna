using UnityEngine;
using UnityEngine.Pool;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin _coinPrefab;
    [SerializeField] private CoinPlacer _coinPlacer;

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
    }

    private void Start()
    {
        while (_coinPlacer.HasSpace)
        {
            _coinPlacer.PlaceCoin(_pool.Get());
        }
    }

    private void OnCoinPickedUp(Coin coin)
    {
        _pool.Release(coin);
    }

    private Coin CreateCoin()
    {
        Coin coin = Instantiate(_coinPrefab);

        coin.PickedUp += OnCoinPickedUp;

        return coin;
    }

    private void Respawn(Coin coin)
    {
        coin.gameObject.SetActive(true);
    }
}
