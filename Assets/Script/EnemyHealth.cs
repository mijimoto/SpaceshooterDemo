using UnityEngine;

public class EnemyHealth : Health
{public static int LivingEnemyCount;

    private void Awake() => LivingEnemyCount++;
    protected override void Die()
    {
        LivingEnemyCount--;
        base.Die();
        Debug.Log("Enemy died");
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
