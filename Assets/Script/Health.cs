using UnityEngine;

public class Health : MonoBehaviour
{
    public GameObject explosionPrefab;
    public int defaultHealthPoint;
    public int healthPoint;
    public System.Action onDead;
public System.Action onHealthChanged;



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
    public void TakeDamage(int damage)
{
if (healthPoint <= 0) return;

healthPoint -= damage;
onHealthChanged?.Invoke();
if (healthPoint <= 0) Die();
}
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        healthPoint = defaultHealthPoint;
        onHealthChanged?.Invoke();
    }




    // Update is called once per frame
    void Update()
    {

    }
}
