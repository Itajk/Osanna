using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 6f;

    private Rigidbody2D _rigidbody;

    public bool IsMoving => _rigidbody.velocity.x != 0;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public float Move(float stepMagnitude)
    {
        float forwardDirection = Mathf.Sign(transform.right.x);
        float moveDistance = stepMagnitude * _speed * forwardDirection;

        _rigidbody.velocity = new Vector2(moveDistance * forwardDirection, _rigidbody.velocity.y);

        return moveDistance;
    }
}
