using UnityEngine;

public class PlayerShootings : MonoBehaviour
{ 
    public GameObject bulletPrefabs;
    public float shootingInterval;
    private float lastBulletTime;

    // Distance from the ship center to each gun (adjust in Inspector)
    public float gunOffset;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
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

        Instantiate(bulletPrefabs, leftPos, Quaternion.identity);
        Instantiate(bulletPrefabs, rightPos, Quaternion.identity);
    }
}
