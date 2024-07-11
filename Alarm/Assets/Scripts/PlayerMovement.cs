using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";

    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _movementSpeed;

    private float _horizontalInput;
    private float _verticalInput;

    private void Update()
    {
        _horizontalInput = Input.GetAxis(HorizontalAxis);
        _verticalInput = Input.GetAxis(VerticalAxis);
    }

    private void FixedUpdate()
    {
        transform.Rotate(_horizontalInput * _rotationSpeed * Time.fixedDeltaTime * Vector3.up);
        transform.Translate(_verticalInput * _movementSpeed * Time.fixedDeltaTime * Vector3.forward);
    }
}
