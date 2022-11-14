using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{

    public Vector2 topRight;
    public Vector2 bottomLeft;

    public GameObject[] enemies;

    public float timeBtwSpawn;
    float timeBefNextSpawn;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeBefNextSpawn -= Time.deltaTime;

        if (timeBefNextSpawn <= 0)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        timeBefNextSpawn = timeBtwSpawn;
        timeBtwSpawn -= 0.1f;

        int rand = Random.Range(0, 4);
        Vector2 pos = new Vector2();

        if (rand == 0)
        {
            pos = new Vector2(topRight.x, Random.Range(-topRight.y, topRight.y));
        }
        if (rand == 1)
        {
            pos = new Vector2(Random.Range(-topRight.x, topRight.x), topRight.y);
        }
        if (rand == 2)
        {
            pos = new Vector2(bottomLeft.x, Random.Range(-bottomLeft.y, bottomLeft.y));
        }
        if (rand == 3)
        {
            pos = new Vector2(Random.Range(-bottomLeft.x, bottomLeft.x), bottomLeft.y);
        }

        pos = new Vector2(pos.x + transform.position.x, pos.y + transform.position.y);

        Instantiate(enemies[Random.Range(0, enemies.Length)], pos, Quaternion.identity);
    }
}
