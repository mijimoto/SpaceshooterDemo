using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public GameObject bulletPrefabs;
public float shootingInterval;
public Vector3 bulletOffset;

private float lastBulletTime;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Input.mousePosition);
        var worldPoint =
        Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPoint.z = 0;
        transform.position = worldPoint;
        
        // firing postion 
    if (Input.GetMouseButton(0))
        {
            UpdateFiring();
        }
    }
private void UpdateFiring()
{
if (Time.time - lastBulletTime > shootingInterval)
{
ShootBullet();
lastBulletTime = Time.time;
}
}

private void ShootBullet()
{
var bullet = Instantiate(bulletPrefabs, transform.position + bulletOffset, transform.rotation);
}
}
