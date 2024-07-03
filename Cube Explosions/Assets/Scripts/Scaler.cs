using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private float _scaleDiviser;

    private void OnEnable()
    {
        _cube.SelfSpawned += OnSelfSpawned;
    }

    private void OnDisable()
    {
        _cube.SelfSpawned -= OnSelfSpawned;
    }

    private void OnSelfSpawned()
    {
        gameObject.transform.localScale /= _scaleDiviser;
    }
}
