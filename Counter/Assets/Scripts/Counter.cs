using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Counter : MonoBehaviour
{
    [SerializeField] private float _countRate;
    [SerializeField] private int _count;

    private Coroutine _countingCoroutine;
    private bool _isCounting = false;

    public event Action CountIncreased;

    public float Count => _count;

    public void ChangeCountingState()
    {
        _isCounting = !_isCounting;

        if (_isCounting)
        {
            _countingCoroutine = StartCoroutine(KeepCounting());
        }
        else
        {
            StopCoroutine(_countingCoroutine);
        }
    }

    private IEnumerator KeepCounting()
    {
        WaitForSeconds wait = new WaitForSeconds(_countRate);

        while (_isCounting)
        {
            _count++;

            CountIncreased();

            yield return wait;
        }
    }
}
