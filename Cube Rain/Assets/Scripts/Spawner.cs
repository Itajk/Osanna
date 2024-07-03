using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Spawnable _spawnable;

    [SerializeField] private float _spawnPointMaxOffset;
    [SerializeField] private float _spawnRate;

    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Spawnable> _pool;

    public void ReturnToPool(Spawnable spawnable)
    {
        if (spawnable != null)
        {
            _pool.Release(spawnable);
        }
    }

    private void Awake()
    {
        _pool = new ObjectPool<Spawnable>(
            createFunc: () => CreateSpawnable(),
            actionOnGet: (spawnable) => ActionOnGet(spawnable),
            actionOnRelease: (spawnable) => spawnable.gameObject.SetActive(false),
            actionOnDestroy: (spawnable) => Destroy(spawnable),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetSpawnable), 0f, _spawnRate);
    }

    private void GetSpawnable()
    {
        _pool.Get();
    }

    private Spawnable CreateSpawnable()
    {
        Spawnable spawnable;

        spawnable = Instantiate(_spawnable);
        spawnable.SetSpawner(this);

        return spawnable;
    }

    private void ActionOnGet(Spawnable spawnable)
    {
        spawnable.gameObject.SetActive(true);
        spawnable.Spawn(GetSpawnPosition());
    }

    private Vector3 GetSpawnPosition()
    {
        float positionX = gameObject.transform.position.x + UnityEngine.Random.Range(-_spawnPointMaxOffset, _spawnPointMaxOffset);
        float positionY = gameObject.transform.position.y;
        float positionZ = gameObject.transform.position.z + UnityEngine.Random.Range(-_spawnPointMaxOffset, _spawnPointMaxOffset);

        return new Vector3(positionX, positionY, positionZ);
    }
}
