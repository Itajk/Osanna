using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpHeight = 4f;

    private Rigidbody2D _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    public void Jump()
    {
        int gravityDiviser = 2;

        _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpHeight - Physics2D.gravity.y / gravityDiviser);
    }
}
