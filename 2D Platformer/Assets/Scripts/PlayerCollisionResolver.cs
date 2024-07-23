using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PlayerCollisionResolver : MonoBehaviour
{
    [SerializeField] private int _groundLayer = 3;
    [SerializeField] private float _groundAllowancePercent = 20f;

    private Collider2D _collider;
    private float _groundAllowance;

    private ContactFilter2D _groundContactFilter;
    List<Collider2D> _groundHits;

    public bool IsGrounded { get; private set; } = false;

    private void Awake()
    {
        int hundredPercent = 100;

        _groundAllowance = 1 - _groundAllowancePercent / hundredPercent;

        _collider = GetComponent<Collider2D>();

        _groundHits = new List<Collider2D>();
        _groundContactFilter = new ContactFilter2D();
        _groundContactFilter.SetLayerMask(LayerMask.GetMask(LayerMask.LayerToName(_groundLayer)));
    }

    private void FixedUpdate()
    {
        if (_collider.OverlapCollider(_groundContactFilter, _groundHits) == 0)
        {
            IsGrounded = false;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D[] contacts;

        if (collision.gameObject.layer == _groundLayer)
        {
            contacts = new ContactPoint2D[collision.contactCount];

            IsGrounded = false;

            collision.GetContacts(contacts);

            foreach (ContactPoint2D contact in contacts)
            {
                if (_collider.bounds.center.y - _collider.bounds.extents.y * _groundAllowance > contact.point.y)
                {
                    IsGrounded = true;
                }
            }
        }
    }
}
