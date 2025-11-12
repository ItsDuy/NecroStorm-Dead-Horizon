using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class PickItem : MonoBehaviour
{
    [SerializeField] private string playerTag = "Player";
    [SerializeField] private Transform pickUpPoint;
    [SerializeField] private bool autoPickOnTouch = true;

    private bool isPickedUp;
    private Transform originalParent;

    void Start()
    {
        originalParent = transform.parent;
        var col = GetComponent<CircleCollider2D>();
        if (col) col.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!autoPickOnTouch || isPickedUp) return;
        if (!other || !other.CompareTag(playerTag)) return;

        Transform targetPoint = pickUpPoint;
        if (targetPoint == null)
        {
            // Try to find a child named "PickUpPoint" on the player
            targetPoint = other.transform.Find("PickUpPoint") ?? other.transform;
        }
        AttachTo(targetPoint);
    }

    public void AttachTo(Transform point)
    {
        if (!point) return;

        isPickedUp = true;
        transform.SetParent(point);
        transform.localPosition = Vector3.zero;
        transform.localRotation = Quaternion.identity;

        var rb2d = GetComponent<Rigidbody2D>();
        if (rb2d)
        {
            rb2d.velocity = Vector2.zero;
            rb2d.angularVelocity = 0f;
            rb2d.bodyType = RigidbodyType2D.Kinematic;
        }
    }

    public void Drop()
    {
        if (!isPickedUp) return;

        isPickedUp = false;
        transform.SetParent(originalParent);

        var rb2d = GetComponent<Rigidbody2D>();
        if (rb2d) rb2d.bodyType = RigidbodyType2D.Dynamic;
    }
}