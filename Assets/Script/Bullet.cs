using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float flySpeed = 10f;
    public int damage = 1;

    private GameObject shooter;

    public void Initialize(GameObject shooter)
    {
        this.shooter = shooter;

        // Ignore collision with shooter
        Collider2D bulletCollider = GetComponent<Collider2D>();
        Collider2D shooterCollider = shooter.GetComponent<Collider2D>();
        if (bulletCollider != null && shooterCollider != null)
        {
            Physics2D.IgnoreCollision(bulletCollider, shooterCollider);
        }
    }

    void Update()
    {
        transform.position += Vector3.up * flySpeed * Time.deltaTime;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only hit objects in Enemy layer
        if (collision.gameObject.layer != LayerMask.NameToLayer("Enemy"))
            return;

        // Apply damage
        var health = collision.GetComponentInParent<Health>();
        if (health != null)
        {
            health.TakeDamage(damage);
           Debug.Log($"Bullet hit: {collision.gameObject.name}, parent hierarchy:");
var parent = collision.transform;
while(parent != null)
{
    Debug.Log($" - {parent.name} (Health? {parent.GetComponent<Health>() != null})");
    parent = parent.parent;
}
        }

        // Destroy bullet after hitting
        Destroy(gameObject);
    }
}
