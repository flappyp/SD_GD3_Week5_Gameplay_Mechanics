using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed;
    private Rigidbody enemyRb;
    private GameObject player;
    public float fallThreshold;
    private ScoreManager scoreManager;


    // Start is called before the first frame update
    void Start()
    {
        enemyRb = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        scoreManager = FindAnyObjectByType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRb.AddForce(lookDirection * speed);

        if (transform.position.y < fallThreshold)
        {
            scoreManager.AddScore(1);
            Destroy(gameObject);
        }
    }
}
