using UnityEngine;
using UnityEngine.Pool;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Enemy _enemyPrefab;
    [SerializeField] private Color _color;

    private ObjectPool<Enemy> _pool;
    private int _poolCapacity = 250;
    private int _poolMaxSize = 500;

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

    public void Spawn()
    {
        _pool.Get();
    }

    private void OnEnemyDied(Enemy enemy)
    {
        _pool.Release(enemy);
    }

    private Enemy CreateEnemy()
    {
        Enemy enemy;
        
        enemy = Instantiate(_enemyPrefab);
        enemy.Died += OnEnemyDied;

        return enemy;
    }

    private void Respawn(Enemy enemy)
    {
        enemy.gameObject.SetActive(true);
        enemy.Initialize(transform.position, _color, _target);
    }
}
