using UnityEngine;

public class BossShooting : MonoBehaviour
{
    [Header("Bullet")]
    public GameObject bulletPrefab;
    public float shootingInterval = 1.0f;
    private float lastShootTime;

    [Header("Gun positions")]
    public float gunOffset = 1.2f; // horizontal offset from boss center
    public float verticalOffset = -0.8f; // spawn slightly below boss center

    [Header("Bullet settings")]
    public Vector2 bulletVelocity = new Vector2(0f, -6f); // default: downward
    public bool ignoreCollisionWithBoss = true;

    void Start()
    {
        lastShootTime = Time.time;
        if (bulletPrefab == null)
        {
            Debug.LogWarning("BossShooting: bulletPrefab not assigned.");
        }
    }

    void Update()
    {
        if (bulletPrefab == null) return;

        if (Time.time - lastShootTime >= shootingInterval)
        {
            Shoot();
            lastShootTime = Time.time;
        }
    }

    void Shoot()
    {
        Vector3 leftPos = transform.position + new Vector3(-gunOffset, verticalOffset, 0f);
        Vector3 rightPos = transform.position + new Vector3(gunOffset, verticalOffset, 0f);

        GameObject leftBullet = Instantiate(bulletPrefab, leftPos, Quaternion.identity);
        GameObject rightBullet = Instantiate(bulletPrefab, rightPos, Quaternion.identity);

        // try to set rigidbody velocity if present
        Rigidbody2D leftRb = leftBullet.GetComponent<Rigidbody2D>();
        Rigidbody2D rightRb = rightBullet.GetComponent<Rigidbody2D>();

        if (leftRb != null) leftRb.linearVelocity = bulletVelocity;
        if (rightRb != null) rightRb.linearVelocity = bulletVelocity;

        // ignore collisions between boss and bullets (if both have colliders)
        if (ignoreCollisionWithBoss)
        {
            Collider2D bossCol = GetComponent<Collider2D>();
            if (bossCol != null)
            {
                Collider2D leftCol = leftBullet.GetComponent<Collider2D>();
                Collider2D rightCol = rightBullet.GetComponent<Collider2D>();

                if (leftCol != null) Physics2D.IgnoreCollision(leftCol, bossCol);
                if (rightCol != null) Physics2D.IgnoreCollision(rightCol, bossCol);
            }
        }
    }
}
