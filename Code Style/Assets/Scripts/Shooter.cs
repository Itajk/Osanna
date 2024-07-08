using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

[RequireComponent(typeof(Rigidbody))]
public class Shooter : MonoBehaviour
{
    [SerializeField] private float _number;
    [SerializeField] private Bullet _bulletPrefab;
    [SerializeField] private Transform _target;
    [SerializeField] private float _shootingSpeed;
    [SerializeField] private int _poolCapacity;
    [SerializeField] private int _poolMaxSize;

    private ObjectPool<Bullet> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Bullet>(
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

    private void Update()
    {

    }

    private IEnumerator Shooting()
    {
        WaitForSeconds wait = new WaitForSeconds(_shootingSpeed);
        bool isEnabled = enabled;

        while (isEnabled)
        {
            var _vector3direction = (_target.position - transform.position).normalized;
            var NewBullet = Instantiate(_bulletPrefab, transform.position + _vector3direction, Quaternion.identity);

            NewBullet.GetComponent<Rigidbody>().transform.up = _vector3direction;
            NewBullet.GetComponent<Rigidbody>().velocity = _vector3direction * _number;

            yield return wait;
        }
    }

    public void ReturnToPool(Bullet bullet)
    {
        if (bullet != null)
        {
            _pool.Release(bullet);
        }
    }

    private Bullet CreateBullet()
    {
        Bullet spawnable;

        spawnable = Instantiate(_bulletPrefab);
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

