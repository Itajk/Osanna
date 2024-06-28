using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CounterView : MonoBehaviour
{
    [SerializeField] private Counter _counter;
    [SerializeField] private TextMeshProUGUI _counterText;

    private void Start()
    {
        UpdateText();
    }

    private void OnEnable()
    {
        _counter.CountIncreased += UpdateText;
    }

    private void OnDisable()
    {
        _counter.CountIncreased -= UpdateText;
    }

    private void UpdateText()
    {
        _counterText.text = _counter.Count.ToString("");
    }
}
