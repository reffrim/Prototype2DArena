using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public static int WaveCount;
    public static int SpawnInterval = 480;
    public static Timer SpawnTimer;

    public GameObject EnemyPrefab;

    int EnemiesToSpawn;
    Transform CubeReferenceTranform;

    // Start is called before the first frame update
    void Start()
    {
        SpawnTimer = new Timer(0);
        EnemiesToSpawn = 1;
        WaveCount = 0;
        CubeReferenceTranform = GameObject.Find("Cube").transform;

        //SpawnEnemy(EnemiesToSpawn);
    }

    // Update is called once per frame
    void Update()
    {
        if (SpawnTimer.Counter > SpawnInterval && !SphereController.IsCubeEaten)
        {
            if (WaveCount == 0)
                EnemiesToSpawn = 1;
            else
                EnemiesToSpawn = WaveCount * 2;
            SpawnEnemy(EnemiesToSpawn);
        }
        SpawnTimer.RunForwardTo(SpawnInterval);
    }

    void SpawnEnemy(int count)
    {
        WaveCount++;
        //Debug.Log(WaveCount);
        for (int i = 0; i < count; i++)
        {
            float x;
            float y;
            float z;

            Vector2 distance;
            do
            {
                x = Random.Range(-6.2f, 6.2f);
                y = Random.Range(-3.4f, 3.4f);
                z = transform.position.z;
                distance.x = x - CubeReferenceTranform.position.x;
                distance.y = y - CubeReferenceTranform.position.y;
            }
            while (((distance.x < 3) && (distance.x > -3)) && ((distance.y < 3) && (distance.y > -3)));
            GameObject.Instantiate(EnemyPrefab, new Vector3(x, y, z), transform.rotation);
        }

    }
}
