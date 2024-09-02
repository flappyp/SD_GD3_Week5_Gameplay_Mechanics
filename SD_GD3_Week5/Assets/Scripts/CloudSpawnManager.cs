using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawnManager : MonoBehaviour
{
    public GameObject[] cloudPrefabs;
    public int cloudIndex;
    private float startDelay = 2f;
    private float spawnInterval = 10f;
    public float spawnRangeY = 10f;
    public float spawnRangeZ = 30f;
    public float spawnX = -70f; 

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomCloud", startDelay, spawnInterval);
    }

    void SpawnRandomCloud()
    {
        
        int cloudIndex = Random.Range(0, cloudPrefabs.Length);

        // Generate random Y and Z positions
        float spawnPosY = Random.Range(-spawnRangeY, spawnRangeY);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        
        Instantiate(cloudPrefabs[cloudIndex], new Vector3(spawnX, spawnPosY, spawnPosZ), cloudPrefabs[cloudIndex].transform.rotation);
    }
}
