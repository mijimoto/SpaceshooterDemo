using UnityEngine;

public class PlayerShootings : MonoBehaviour
{ 
    public GameObject bulletPrefabs;
    public float shootingInterval;
    private float lastBulletTime;

    // Distance from the ship center to each gun
    public float gunOffset;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            if (Time.time - lastBulletTime > shootingInterval)
            {
                ShootBullet();
                lastBulletTime = Time.time;
            }
        }
    }

    private void ShootBullet()
    {
        // Spawn bullets with left and right offset from ship center
        Vector3 leftPos = transform.position + new Vector3(-gunOffset, 0f, 0f);
        Vector3 rightPos = transform.position + new Vector3(gunOffset, 0f, 0f);

        GameObject leftBullet = Instantiate(bulletPrefabs, leftPos, Quaternion.identity);
        GameObject rightBullet = Instantiate(bulletPrefabs, rightPos, Quaternion.identity);

        // Ignore collision between player and bullets
        Collider2D playerCol = GetComponent<Collider2D>();
        Collider2D leftCol = leftBullet.GetComponent<Collider2D>();
        Collider2D rightCol = rightBullet.GetComponent<Collider2D>();

        if (playerCol != null)
        {
            if (leftCol != null) Physics2D.IgnoreCollision(leftCol, playerCol);
            if (rightCol != null) Physics2D.IgnoreCollision(rightCol, playerCol);
        }
    }
}
