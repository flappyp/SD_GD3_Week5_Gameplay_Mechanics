using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float jumpForce = 10f;
    private bool isGrounded = true;
    private GameObject focalPoint;
    public bool hasPowerUp;
    public bool hasJumpPowerUp;
    public float powerupStrength;
    public GameObject powerupIndicator;
    public GameObject jumpPowerupIndicator;
    private ScoreManager scoreManager;
    public AudioClip coinSound;
    public AudioClip knockbackSound;
    public AudioClip jumppowerSound;
    public AudioClip gemSound;
    private AudioSource audioSource;
   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
        scoreManager = FindObjectOfType<ScoreManager>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 moveDirection = new Vector3(horizontalInput, forwardInput).normalized;

        rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        rb.AddForce(focalPoint.transform.right * speed * horizontalInput);


        // Allow jumping only when the player has the jump power-up and is grounded
        if (hasJumpPowerUp && Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce * 2, ForceMode.Impulse);
            isGrounded = false;  // The player is no longer grounded after jumping
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Knockback"))
        {
            hasPowerUp = true;
            audioSource.PlayOneShot(knockbackSound);
            Destroy(other.gameObject);
            StartCoroutine(KnockbackCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);
        }
        else if (other.CompareTag("JumpPowerup"))
        {
            hasJumpPowerUp = true;  // Set the jump power-up to true when collected
            audioSource.PlayOneShot(jumppowerSound);
            Destroy(other.gameObject);
            StartCoroutine(JumpPowerupCountdownRoutine());
            jumpPowerupIndicator.gameObject.SetActive(true);
        }
        if(other.CompareTag("Coin"))
        {
            audioSource.PlayOneShot(coinSound);
            scoreManager.AddScore(1);
            Destroy(other.gameObject);
        }
        if (other.CompareTag("BigGem"))
        {
            audioSource.PlayOneShot(gemSound);
            scoreManager.AddScore(20);
            Destroy(other.gameObject);
        }
    }

    IEnumerator KnockbackCountdownRoutine()
    {
        yield return new WaitForSeconds(7);
        hasPowerUp = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator JumpPowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(10);
        hasJumpPowerUp = false;  // Reset the jump power-up after the duration ends
        jumpPowerupIndicator.gameObject.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;  // The player is grounded after colliding with the ground
        }
        else if (collision.gameObject.CompareTag("Enemy") && hasPowerUp)
        {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
}
