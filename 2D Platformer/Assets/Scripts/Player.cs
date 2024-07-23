using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private InputReader _inputReader;
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private PlayerCollisionResolver _collisionResolver;
    [SerializeField] private Animator _animator;
    [SerializeField] private CoinCollector _coinPicker;
    [SerializeField] private int _coins = 0;

    private void OnEnable()
    {
        _coinPicker.CoinPickedUp += OnCoinPickedUp;
    }

    private void FixedUpdate()
    {
        if (_mover.Move(_inputReader.HorizontalInput) != 0)
        {
            transform.forward = new Vector3(0, 0, Mathf.Sign(_inputReader.HorizontalInput));
        }

        if (_inputReader.JumpInput > 0 && _collisionResolver.IsGrounded)
        {
            _jumper.Jump();
        }

        _animator.SetBool(nameof(_mover.IsMoving), _mover.IsMoving);
    }

    private void OnCoinPickedUp()
    {
        _coins++;
    }
}
