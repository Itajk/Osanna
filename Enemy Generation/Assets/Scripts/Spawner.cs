using UnityEngine;
using UnityEngine.Pool;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Spawnable _spawnable;
    [SerializeField] private float _spawnRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private List<Color> _spawnPointsColors;

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

    private void Start()
    {
        if (_spawnPoints.Count > 0)
        {
            _spawningCoroutine = StartCoroutine(KeepSpawning());
        }
    }

    public void ReturnToPool(Spawnable spawnable)
    {
        if (spawnable != null)
        {
            _pool.Release(spawnable);
        }
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
        int spawnPointIndex;

        spawnPointIndex = UnityEngine.Random.Range(0, _spawnPoints.Count);

        spawnable.gameObject.SetActive(true);
        spawnable.Initialize(_spawnPoints[spawnPointIndex].position, _spawnPointsColors[spawnPointIndex], GetMovementDirection());
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

    private Vector3 GetMovementDirection()
    {
        Vector2 circularDirection;

        circularDirection = UnityEngine.Random.insideUnitCircle;

        return new Vector3(circularDirection.x, 0, circularDirection.y);
    }
}

