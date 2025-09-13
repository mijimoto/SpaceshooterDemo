using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 20f;        // Speed of following
    public float yOffset = 1f;           // Distance above the finger
    public float shootingInterval;
    public Vector3 bulletOffset;

    private float lastBulletTime;
    private Vector2 minBounds;
    private Vector2 maxBounds;
    private float playerZDistance;

    void Start()
    {
        // How far the player is from the camera in world units
        playerZDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
    }

    void Update()
    {
        // Update bounds every frame (since camera scrolls)
        Vector3 bottomLeft = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, playerZDistance));
        Vector3 topRight   = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, playerZDistance));

        minBounds = bottomLeft;
        maxBounds = topRight;

        Vector3 targetPos;

        if (Input.touchCount > 0) // Phone
        {
            Touch touch = Input.GetTouch(0);
            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(touch.position.x, touch.position.y, playerZDistance));
        }
        else // PC testing
        {
            targetPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, playerZDistance));
        }

        targetPos.z = transform.position.z; // Keep Z fixed
        targetPos.y += yOffset;

        // Clamp inside screen bounds
        targetPos.x = Mathf.Clamp(targetPos.x, minBounds.x, maxBounds.x);
        targetPos.y = Mathf.Clamp(targetPos.y, minBounds.y, maxBounds.y);

        // Smooth follow
        transform.position = Vector3.Lerp(transform.position, targetPos, moveSpeed * Time.deltaTime);
    }
}
