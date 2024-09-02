using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using TMPro;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefab;
    public int enemyIndex;
    public float spawnRangeX = 10f; 
    public float spawnRangeZ = 10f;
    public float spawnInterval = 0f;
    public int waveNumber = 1;
    public GameObject[] powerupPrefab;
    public int powerupIndex;
    public TMP_Text waveText;
    public int enemyCount;

    // Start is called before the first frame update
    void Start()
    {
        UpdateWaveText();
       
        int powerupIndex = Random.Range(0, powerupPrefab.Length);
        waveNumber++;
        SpawnEnemyWave(waveNumber);
        Instantiate(powerupPrefab[powerupIndex], GenerateSpawnPosition(), powerupPrefab[powerupIndex].transform.rotation);
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        { 
            int enemyIndex = Random.Range(0, powerupPrefab.Length);
            Instantiate(enemyPrefab[enemyIndex], GenerateSpawnPosition(), enemyPrefab[enemyIndex].transform.rotation);
        }
    }
    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        { 
            Instantiate(powerupPrefab[powerupIndex], GenerateSpawnPosition(), powerupPrefab[powerupIndex].transform.rotation);
            waveNumber++; SpawnEnemyWave(waveNumber);
            UpdateWaveText();
        
        }

    }
    private Vector3 GenerateSpawnPosition()
    {
        
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 randomPos = new Vector3(spawnPosX, -1, spawnPosZ);
        return randomPos;
    }
    void UpdateWaveText()
    {
        waveText.text = "Wave: " + waveNumber;
    }
}
