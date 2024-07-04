using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Spawnable _spawnable;
    [SerializeField] private float _spawnPointMaxOffset;
    [SerializeField] private float _spawnRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Spawnable> _pool;
    private Coroutine _spawningCoroutine;

    private void Awake()
    {
        _pool = new ObjectPool<Spawnable>(
            createFunc: () => CreateSpawnable(),
            actionOnGet: (spawnable) => Respawn(spawnable),
            actionOnRelease: (spawnable) => spawnable.gameObject.SetActive(false),
            actionOnDestroy: (spawnable) => Destroy(spawnable),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void OnEnable()
    {
        _spawningCoroutine = StartCoroutine(KeepSpawning());
    }

    private void OnDisable()
    {
        StopCoroutine(_spawningCoroutine);
    }

    public void ReturnToPool(Spawnable spawnable)
    {
        if (spawnable != null)
        {
            _pool.Release(spawnable);
        }
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

    private void Respawn(Spawnable spawnable)
    {
        spawnable.gameObject.SetActive(true);
        spawnable.Initialize(GetSpawnPosition());
    }

    private IEnumerator KeepSpawning()
    {
        WaitForSeconds wait = new WaitForSeconds(_spawnRate);

        while (true)
        {
            _pool.Get();

            yield return wait;
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float positionX = gameObject.transform.position.x + UnityEngine.Random.Range(-_spawnPointMaxOffset, _spawnPointMaxOffset);
        float positionY = gameObject.transform.position.y;
        float positionZ = gameObject.transform.position.z + UnityEngine.Random.Range(-_spawnPointMaxOffset, _spawnPointMaxOffset);

        return new Vector3(positionX, positionY, positionZ);
    }
}
