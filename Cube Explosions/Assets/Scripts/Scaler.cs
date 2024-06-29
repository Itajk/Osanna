using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private float _scaleDiviser;

    public void Rescale()
    {
        transform.localScale = transform.localScale / _scaleDiviser;
    }
}
