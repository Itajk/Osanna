using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class Shooter : MonoBehaviour
{
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _shootingTarget;
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Bullet> _bulletPool;

    private void Awake()
    {
        _bulletPool = new ObjectPool<Bullet>(
            createFunc: () => CreateBullet(),
            actionOnGet: (bullet) => Respawn(bullet),
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet),
            collectionCheck: true,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize
            );
    }

    private void Start()
    {
        StartCoroutine(Shooting());
    }

    private IEnumerator Shooting()
    {
        WaitForSeconds wait = new WaitForSeconds(_shootingSpeed);

        while (enabled)
        {
            _bulletPool.Get();

            yield return wait;
        }
    }

    private Bullet CreateBullet()
    {
        Bullet bullet;

        bullet = Instantiate(_bulletPrefab);
        bullet.Died += OnBulletDied;

        return bullet;
    }

    private void OnBulletDied(Bullet bullet)
    {
        _bulletPool.Release(bullet);
    }

    private void Respawn(Bullet bullet)
    {
        bullet.gameObject.SetActive(true);
         bullet.Initialize(transform.position + (_shootingTarget.position - transform.position).normalized);
    }
}

