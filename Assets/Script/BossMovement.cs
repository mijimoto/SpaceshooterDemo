using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BossMovement : MonoBehaviour
{
    [Header("Targeting")]
    public Transform playerTransform;
    [Header("Movement (X axis)")]
    public float horizontalSpeed = 4f;
    public float smoothTime = 0.08f; 
    public float padding = 0.5f; // padding from left/right edges

    [Header("Vertical (auto ascend)")]
    public float verticalSpeed = 2f;
    public bool ascend = true; 

    Rigidbody2D rb;
    float velocityX;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        if (playerTransform == null)
        {
            var playerGO = GameObject.FindGameObjectWithTag("Player");
            if (playerGO != null) playerTransform = playerGO.transform;
        }
    }

    void FixedUpdate()
    {
        // --- Camera bounds for X ---
        Camera cam = Camera.main;
        float minX = -Mathf.Infinity, maxX = Mathf.Infinity;

        if (cam != null)
        {
            float zDistance = Mathf.Abs(cam.transform.position.z - transform.position.z);
            Vector3 leftWorld = cam.ViewportToWorldPoint(new Vector3(0f, 0.5f, zDistance));
            Vector3 rightWorld = cam.ViewportToWorldPoint(new Vector3(1f, 0.5f, zDistance));
            minX = leftWorld.x + padding;
            maxX = rightWorld.x - padding;
        }

        // --- X targeting (smooth towards player.x if available) ---
        float currentX = rb.position.x;
        float targetX = currentX;

        if (playerTransform != null)
        {
            targetX = Mathf.Clamp(playerTransform.position.x, minX, maxX);
        }

        float newX = Mathf.SmoothDamp(currentX, targetX, ref velocityX, smoothTime, horizontalSpeed, Time.fixedDeltaTime);

        // --- Y movement: auto ascend if enabled ---
        float newY = rb.position.y;
        if (ascend)
        {
            newY += verticalSpeed * Time.fixedDeltaTime;
        }

        // --- Apply movement via physics ---
        rb.MovePosition(new Vector2(newX, newY));
    }

    // Optional: call to force re-find player
    public void FindPlayerByTag(string tag = "Player")
    {
        var playerGO = GameObject.FindGameObjectWithTag(tag);
        if (playerGO != null) playerTransform = playerGO.transform;
    }
}
