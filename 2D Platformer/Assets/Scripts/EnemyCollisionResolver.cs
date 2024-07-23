using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class EnemyCollisionResolver : MonoBehaviour
{
    [SerializeField] private int _groundLayer = 3;

    private Collider2D _collider;

    public bool IsOnEdge { get; private set; } = false;
    public bool IsOnDeadEnd { get; private set; } = false;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        ContactPoint2D[] contacts;

        if (collision.gameObject.layer == _groundLayer)
        {
            contacts = new ContactPoint2D[collision.contactCount];

            if (collision.GetContacts(contacts) > 0)
            {
                IsOnEdge = GetIsOnEdge(contacts);
                IsOnDeadEnd = GetIsOnDeadEnd(contacts);
            }
        }
    }

    private bool GetIsOnEdge(ContactPoint2D[] contacts)
    {
        bool isOnEdge = true;
        int leftSetCount = 0;
        int rightSetCount = 0;
        int extentsDiviser = 2;

        for (int i = 0; i < contacts.Length; i++)
        {
            if (contacts[i].point.y < _collider.bounds.center.y - _collider.bounds.extents.y / extentsDiviser)
            {
                if (contacts[i].point.x < _collider.bounds.center.x)
                {
                    leftSetCount++;
                }
                else if (contacts[i].point.x > _collider.bounds.center.x)
                {
                    rightSetCount++;
                }
            }
        }

        if (leftSetCount > 0 && rightSetCount > 0)
        {
            isOnEdge = false;
        }

        return isOnEdge;
    }

    private bool GetIsOnDeadEnd(ContactPoint2D[] contacts)
    {
        bool isOnDeadEnd = false;
        float extentsMultiplier = 0.9f;

        for (int i = 0; i < contacts.Length; i++)
        {
            if (contacts[i].point.x > _collider.bounds.center.x + _collider.bounds.extents.x * extentsMultiplier ||
                contacts[i].point.x < _collider.bounds.center.x - _collider.bounds.extents.x * extentsMultiplier)
            {
                isOnDeadEnd = true;
            }
        }

        return isOnDeadEnd;
    }
}
