using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class Spawner : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Color _color;
    [SerializeField] private float _spawnRate;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Enemy> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Enemy>(
            createFunc: () => CreateEnemy(),
            actionOnGet: (enemy) => Respawn(enemy),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        StartCoroutine(KeepSpawning());
    }

    public void ReturnToPool(Enemy enemy)
    {
        if (enemy != null)
        {
            _pool.Release(enemy);
        }
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy;

        enemy = Instantiate(_enemyPrefab);
        enemy.SetSpawner(this);
        enemy.SetTarget(_target);

        return enemy;
    }

    private void Respawn(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.Initialize(transform.position, _color);
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
}

