using UnityEngine;

public class CoinSpawnPath : MonoBehaviour
{
    [SerializeField] private Transform _start;
    [SerializeField] private Transform _end;

    private Transform _currentWaypoint;
    private float offset = 1f;

    public bool IsEndReached => _end.position == _currentWaypoint.position;

    private void Awake()
    {
        _currentWaypoint = _start;
    }

    public void PlaceCoin(Coin coin)
    {
        coin.Initialize(_currentWaypoint.position);

        _currentWaypoint.Translate(offset, 0, 0);
    }
}
