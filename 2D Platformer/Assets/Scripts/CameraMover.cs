using UnityEngine;

public class CameraMover : MonoBehaviour
{
    [SerializeField] private Transform _anchor;

    private void Update()
    {
        transform.position = new Vector3(_anchor.transform.position.x, _anchor.transform.position.y, transform.position.z);
    }
}
