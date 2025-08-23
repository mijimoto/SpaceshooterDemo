using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
public GameObject explosionPrefab;

private void OnTriggerEnter2D(Collider2D collision) => Die();

private void Die()
{
var explosion = Instantiate(explosionPrefab, transform.position,
transform.rotation);
Destroy(explosion, 1);
Destroy(gameObject);
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
