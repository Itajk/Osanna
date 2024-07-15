using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider2D), typeof(Rigidbody2D))]
public class Player : MonoBehaviour
{
    private const string HorizontalAxis = "Horizontal";
    private const string JumpAxis = "Jump";
    private const string IsMovingVariableName = "IsMoving";

    [SerializeField] private float _jumpForce;
    [SerializeField] private float _movementSpeed;
    [SerializeField] private int _coins;

    private float _horizontalInput;
    private float _jumpInput;
    private Animator _animator;
    private Rigidbody2D _rigidbody;
    private float _deltaX = 0;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis(HorizontalAxis);
        _jumpInput = Input.GetAxis(JumpAxis);

        _animator.SetBool(IsMovingVariableName, Mathf.Abs(_deltaX) > 0);
    }

    private void FixedUpdate()
    {
        _deltaX = _horizontalInput * _movementSpeed * Time.fixedDeltaTime * transform.right.x;

        if (_deltaX < 0)
        {
            transform.rotation *= Quaternion.FromToRotation(Vector2.right, Vector2.left);
        }

        transform.Translate(_deltaX, 0, 0);

        if (_jumpInput > 0 && _rigidbody.velocity.y == 0)
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce);
        }
    }

    public void AddCoin()
    {
        _coins++;
    }
}
