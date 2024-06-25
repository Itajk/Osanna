using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Vector3 _scalingDirection;
    [SerializeField] private float _scalingSpeed;

    void Update()
    {
        transform.localScale += _scalingDirection * _scalingSpeed;
    }
}