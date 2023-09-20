using UnityEngine;

public class SpawnEntityManager : MonoBehaviour
{
    [SerializeField] GameObject prefabEnemy;
    private Vector2 startSpawnZone;
    private Vector2 endSpawnZone;

    public void Start()
    {
        startSpawnZone = new Vector2(-11, -11);
        endSpawnZone = new Vector2(11, 11);
        Spawn(prefabEnemy, 3);
    }
    public void Spawn(GameObject prefabEnemy, int count)
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 spawnPoint = new Vector3(Random.Range(startSpawnZone.x, endSpawnZone.x),
                Random.Range(startSpawnZone.y, endSpawnZone.y), 1);
            Instantiate(prefabEnemy, spawnPoint, Quaternion.identity);
        }
    }
}
