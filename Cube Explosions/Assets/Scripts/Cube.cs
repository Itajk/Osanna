using System;
using UnityEngine;

[RequireComponent(typeof(Renderer), typeof(Rigidbody))]
public class Cube : MonoBehaviour
{
    [SerializeField] private float _scaleDiviser;

    [SerializeField] private float _spawnChancePercent;
    [SerializeField] private float _spawnChanceDiviser;

    private Renderer _renderer;
    private Rigidbody _rigidbody;

    public event Action CubeClicked;

    public Rigidbody Rigidbody => _rigidbody;

    public void Initialize()
    {
        gameObject.transform.localScale /= _scaleDiviser;
        _renderer.material.color = new Color(UnityEngine.Random.value, UnityEngine.Random.value, UnityEngine.Random.value);
        _rigidbody.useGravity = true;
        _spawnChancePercent /= _spawnChanceDiviser;
    }

    public bool RollSpawnChance()
    {
        float minChancePercent = 0;
        float maxChancePercent = 100;

        return _spawnChancePercent > UnityEngine.Random.Range(minChancePercent, maxChancePercent - 1);
    }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void OnMouseUpAsButton()
    {
        CubeClicked?.Invoke();

        Destroy(gameObject);
    }
}
