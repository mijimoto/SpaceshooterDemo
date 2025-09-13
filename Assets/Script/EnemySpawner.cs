using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour
{
    [Header("JSON")]
    [Tooltip("Name of the JSON file (without extension) inside Resources folder, default: waves.json")]
    public string jsonFileName = "waves";

    [Header("Boss")]
    public GameObject bossPrefab;
    public float bossSpawnDelay = 5f;
    public float bossSpawnYOffset = 1f; // extra offset above camera top (optional)

    private WaveJsonData[] waves;
    private int currentWave = 0;
    private bool bossSpawned = false;

    void Start()
    {
        LoadWavesFromJson();
        if (waves == null || waves.Length == 0)
        {
            Debug.LogWarning("EnemySpawner: No waves found in JSON.");
            return;
        }
        StartCoroutine(SpawnNextWaveRoutine());
    }

    void LoadWavesFromJson()
    {
        TextAsset txt = Resources.Load<TextAsset>(jsonFileName);
        if (txt == null)
        {
            Debug.LogError($"EnemySpawner: Could not find Resources/{jsonFileName}.json");
            waves = new WaveJsonData[0];
            return;
        }

        try
        {
            WaveJsonContainer container = JsonUtility.FromJson<WaveJsonContainer>(txt.text);
            if (container != null && container.waves != null)
            {
                waves = container.waves;
            }
            else
            {
                Debug.LogError("EnemySpawner: JSON parsing returned no waves.");
                waves = new WaveJsonData[0];
            }
        }
        catch (System.Exception ex)
        {
            Debug.LogError("EnemySpawner: Failed to parse JSON: " + ex.Message);
            waves = new WaveJsonData[0];
        }
    }

    IEnumerator SpawnNextWaveRoutine()
    {
        while (waves != null && currentWave < waves.Length)
        {
            SpawnWave(waves[currentWave]);
            float delay = waves[currentWave].nextWaveDelay;
            currentWave++;
            // wait for delay before spawning next
            yield return new WaitForSeconds(delay);
        }

        // All waves done -> spawn boss after bossSpawnDelay (once)
        if (!bossSpawned)
        {
            bossSpawned = true;
            yield return new WaitForSeconds(bossSpawnDelay);
            SpawnBoss();
        }
    }

    void SpawnWave(WaveJsonData wave)
    {
        if (wave == null) return;

        // Load enemy prefab
        GameObject enemyPrefab = Resources.Load<GameObject>(wave.enemyPrefabPath);
        if (enemyPrefab == null)
        {
            Debug.LogError($"EnemySpawner: Could not load enemy prefab at {wave.enemyPrefabPath}");
            return;
        }

        // Load fly path prefab
        GameObject flyPathPrefab = Resources.Load<GameObject>(wave.flyPathPrefabPath);
        if (flyPathPrefab == null)
        {
            Debug.LogError($"EnemySpawner: Could not load fly path prefab at {wave.flyPathPrefabPath}");
            return;
        }

        // Instantiate fly path in the scene
        GameObject flyPathInstance = Instantiate(flyPathPrefab);
        Vector2[] waypoints = ExtractWaypoints(flyPathInstance);

        // Spawn enemies along formation offset
        Vector3 startPosition = waypoints.Length > 0 ? (Vector3)waypoints[0] : transform.position;

        for (int i = 0; i < wave.numberOfEnemy; i++)
        {
            GameObject enemy = Instantiate(enemyPrefab, startPosition, Quaternion.identity);
            FlyPathAgent agent = enemy.GetComponent<FlyPathAgent>();
            if (agent != null)
            {
                FlyPath flyPath = flyPathInstance.GetComponent<FlyPath>();
                agent.flyPath = flyPath;
                agent.flySpeed = wave.speed;
            }

            startPosition += (Vector3)wave.formationOffset;
        }

    }

    private Vector2[] ExtractWaypoints(GameObject pathGO)
    {
        int count = pathGO.transform.childCount;
        Vector2[] points = new Vector2[count];
        for (int i = 0; i < count; i++)
        {
            points[i] = pathGO.transform.GetChild(i).position;
        }
        return points;
    }


    private void SpawnBoss()
    {
        if (bossPrefab == null)
        {
            Debug.LogWarning("EnemySpawner: bossPrefab not assigned.");
            return;
        }

        Vector3 spawnPos = CalculateTopCenterSpawn();
        Instantiate(bossPrefab, spawnPos, Quaternion.identity);
    }

    private Vector3 CalculateTopCenterSpawn()
    {
        if (Camera.main == null)
        {
            return transform.position + Vector3.up * 6f;
        }

        float zDistance = Mathf.Abs(Camera.main.transform.position.z - transform.position.z);
        Vector3 topCenter = Camera.main.ViewportToWorldPoint(new Vector3(0.5f, 1f, zDistance));
        topCenter.y += bossSpawnYOffset;
        topCenter.z = 0f;
        return topCenter;
    }
}
