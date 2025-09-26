using System.Collections;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] EnemyTypes;
    [SerializeField] private GameObject[] EnemiesToSpawn;
    [SerializeField] private float spawnCooldown = 1;
    [SerializeField] private Vector2 spawnLocationOffset;

    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    public void SpawnEnemy(GameObject enemyType)
    {
        Instantiate(enemyType, (Vector2)cam.position + spawnLocationOffset, Quaternion.identity);
    }

    public void SpawnEnemy(GameObject enemyType, Vector2 position)
    {
        Instantiate (enemyType, position, Quaternion.identity);
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            return;
        }

        StartCoroutine(SpawnTimer());

        GetComponent<BoxCollider2D>().enabled = false;
    }

    private int index;
    IEnumerator SpawnTimer()
    {
        if(EnemiesToSpawn.Length > 0)
            SpawnEnemy(EnemiesToSpawn[index]);

        Debug.Log("enemy spawned");
        yield return new WaitForSeconds(spawnCooldown);
        if (index < EnemiesToSpawn.Length - 1)
        {
            index++;
            spawnLocationOffset.x *= -1;
            StartCoroutine(SpawnTimer());
        }
        else
        {
            StopCoroutine(SpawnTimer());
        }
    }

}
