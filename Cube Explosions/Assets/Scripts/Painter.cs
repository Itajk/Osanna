using UnityEngine;

public class Painter : MonoBehaviour
{
    [SerializeField] private Cube _cube;

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
        gameObject.GetComponent<Renderer>().material.color = GetRandomColor();
    }

    private Color GetRandomColor()
    {
        float redComponent;
        float greenComponent;
        float blueComponent;

        redComponent = UnityEngine.Random.Range(0.000f, 1.000f);
        greenComponent = UnityEngine.Random.Range(0.000f, 1.000f);
        blueComponent = UnityEngine.Random.Range(0.000f, 1.000f);

        return new Color(redComponent, greenComponent, blueComponent);
    }
}
