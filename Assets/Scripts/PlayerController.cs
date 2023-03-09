using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Animator playerAnim;
    private AudioSource playerAudio;
    private MoveLeft moveLeft;
    public ParticleSystem explosionParticle;
    public ParticleSystem dirtParticle;
    public AudioClip soundEffect;
    public AudioClip crashSound;
    public float jumpForce = 10;
    public float gravityModifier;
    private int doubleJump = 0;
    public bool isOnGround = true;
    public bool gameOver;
    private float score = 0;
    public bool startTheGame = false;
    private float walkAnimationValue = 1.5f;
    private float runAnimationValue = 2;
    public bool dash;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        playerAnim = GetComponent<Animator>();
        playerAudio = GetComponent<AudioSource>();
        moveLeft = GetComponent<MoveLeft>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x < 0)
        {
            transform.Translate(new Vector3(0, 0, 3) * Time.deltaTime);
            playerAnim.SetFloat("Speed_f", 0.5f);
            dirtParticle.Stop();
        }
        else
        {
            playerAnim.SetFloat("Speed_f", 1.5f);
            startTheGame = true;
        }

        if (startTheGame)
        {
            if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
            {
                playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                playerAnim.SetTrigger("Jump_trig");
                dirtParticle.Stop();
                playerAudio.PlayOneShot(soundEffect, 1.0f);
                doubleJump++;

                if (doubleJump == 2)
                {
                    isOnGround = false;
                }
            }

            if (dash && !gameOver)
            {
                playerAnim.speed = runAnimationValue;
                score += (int)Time.deltaTime + 5;
            }
            else if (!dash && !gameOver)
            {
                playerAnim.speed = walkAnimationValue;
                score += (int)Time.deltaTime + 1;
            }
        }
        Debug.Log($"Score: {score}");
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isOnGround = true;
            dirtParticle.Play();
            doubleJump = 0;
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            playerAudio.PlayOneShot(crashSound, 1.0f);
            explosionParticle.Play();
            dirtParticle.Stop();
            playerAnim.SetBool("Death_b", true);
            playerAnim.SetInteger("DeathType_int", 1);
            Debug.Log($"Game Over! Reached score: {score}");
            gameOver = true;
        }
    }
}
