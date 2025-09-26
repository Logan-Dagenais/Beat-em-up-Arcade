using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyTypes;

    public void SpawnEnemy(GameObject enemyType, Vector2 position)
    {
        Instantiate(enemyType, position, Quaternion.identity);
    }

    public void DebugSpawnMelee()
    {
        SpawnEnemy(EnemyTypes[0], Vector2.zero);
    }

    public void DebugSpawnRange()
    {
        SpawnEnemy(EnemyTypes[1], Vector2.zero);
    }

    public void DebugSpawnHeavy()
    {
        SpawnEnemy(EnemyTypes[2], Vector2.zero);
    }
}
