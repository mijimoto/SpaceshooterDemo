using UnityEngine;

[System.Serializable]
public class WaveJsonData
{
    public string enemyPrefabPath;       // e.g. "Prefabs/EnemySmall"
    public string flyPathPrefabPath;     // e.g. "Prefabs/Paths/SwoopPath"
    public int numberOfEnemy;
    public Vector2 formationOffset;
    public float speed;
    public float nextWaveDelay;
}

[System.Serializable]
public class WaveJsonContainer
{
    public WaveJsonData[] waves;
}
