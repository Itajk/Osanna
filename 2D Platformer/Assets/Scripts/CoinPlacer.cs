using UnityEngine;
using System.Collections.Generic;

public class CoinPlacer : MonoBehaviour
{
    [SerializeField] private float _gap;
    [SerializeField] private Transform[] _paths;

    private Queue<Vector2> _positions;

    public bool HasSpace => _positions.Count > 0;

    private void Awake()
    {
        Vector2 currentPoint;
        Vector2 nextPoint;
        Vector2 direction;
        float distance;
        float lastGap = _gap;

        _positions = new Queue<Vector2>();

        for (int i = 0; i < _paths.Length; i++)
        {
            _positions.Enqueue(_paths[i].GetChild(0).position);

            for (int j = 0; j < _paths[i].childCount - 1; j++)
            {
                currentPoint = _paths[i].GetChild(j).position;
                nextPoint = _paths[i].GetChild(j + 1).position;
                distance = (nextPoint - currentPoint).magnitude;
                direction = (nextPoint - currentPoint).normalized;

                if (distance >= lastGap)
                {
                    distance -= lastGap;
                    currentPoint += direction * lastGap;

                    _positions.Enqueue(currentPoint);
                }

                while (distance >= _gap)
                {
                    distance -= _gap;
                    currentPoint += direction * _gap;

                    _positions.Enqueue(currentPoint);
                }

                lastGap = _gap - distance;
            }
        }
    }

    public void PlaceCoin(Coin coin)
    {
        coin.Initialize(_positions.Dequeue());
    }
}
