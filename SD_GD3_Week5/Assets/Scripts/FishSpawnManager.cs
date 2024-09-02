using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawnManager : MonoBehaviour
{
    public GameObject[] fishPrefabs;
    public int fishIndex;
    private float startDelay = 2f;
    private float spawnInterval = 10f;
    public float spawnY = -25f;
    public float spawnRangeZ = 30f;
    public float spawnX = 70f;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SpawnRandomFish", startDelay, spawnInterval);
    }

    // Update is called once per frame
    void SpawnRandomFish()
    {
        int fishIndex = Random.Range(0, fishPrefabs.Length);

        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);

        Instantiate(fishPrefabs[fishIndex], new Vector3(spawnX, spawnY, spawnPosZ), fishPrefabs[fishIndex].transform.rotation);
    }
}
