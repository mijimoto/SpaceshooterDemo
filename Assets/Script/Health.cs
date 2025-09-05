using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int defaultHealthPoint;
    private int healthPoint;
    public System.Action onDead;

private void Awake()
{
    healthPoint = defaultHealthPoint;
}
    protected virtual void Die()
    {
    var explosion = Instantiate(explosionPrefab, transform.position, transform.rotation);
Destroy(explosion, 1);
Destroy(gameObject);
onDead?.Invoke();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start() => healthPoint = defaultHealthPoint;
public void TakeDamage(int damage)
{
    damage = Mathf.Max(damage, 1); // minimum 1 damage
    healthPoint -= damage;
    Debug.Log($"{gameObject.name} took {damage} damage, HP left: {healthPoint}");

    if (healthPoint <= 0) Die();
}



    // Update is called once per frame
    void Update()
    {

    }
}
