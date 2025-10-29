using System.Collections;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnemySpawner : MonoBehaviour
{
    static public int TotalEnemyCount;
    static public bool Encounter;
    public const int MaxEnemies = 10;

    [SerializeField] private GameObject[] EnemyTypes;
    [SerializeField] private GameObject[] EnemiesToSpawn;
    [SerializeField] private float spawnCooldown = 1;
    [SerializeField] private Vector2 spawnLocationOffset;
    [SerializeField] private Collider2D fightCamCollider;
    [SerializeField] private Collider2D normalCamCollider;
    [SerializeField] private GameObject cinemachineCam;

    private Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;

        if (transform.childCount != 0)
        {
            fightCamCollider = transform.GetChild(0).GetComponent<Collider2D>();
        }
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
        //StartCoroutine(SpawnTimer());
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

        if (fightCamCollider)
        {
            fightCamCollider.gameObject.SetActive(true);
            cinemachineCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = fightCamCollider; //changes cam to one spot
        }
    }

    private void DestroySpawner()
    {
        cinemachineCam.GetComponent<CinemachineConfiner2D>().BoundingShape2D = normalCamCollider; //changes the cam confiner to the normal one
        Encounter = false;
        Destroy(gameObject);
    }

    private int index;
    IEnumerator SpawnTimer()
    {
        //Debug.Log("enemy spawned");
        yield return new WaitForSeconds(spawnCooldown);

        if (TotalEnemyCount < MaxEnemies &&
            index < EnemiesToSpawn.Length)
        {
            SpawnEnemy(EnemiesToSpawn[index]);

            spawnLocationOffset.x *= -1;

            index++;

            TotalEnemyCount++;
        }

        StartCoroutine(SpawnTimer());
        
        if (TotalEnemyCount <= 0)
        {
            StopCoroutine(SpawnTimer());
            DestroySpawner();
        }
    }

}
