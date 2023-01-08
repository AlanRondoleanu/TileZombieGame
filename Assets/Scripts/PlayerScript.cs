using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    public float speed;
    public float fireRate;
    public GameObject shot;
    public bool alive = true;
    public AudioClip[] sounds;

    private Rigidbody2D rb;
    private float nextFire;
    private Camera mainCam;
    private AudioSource audioSrcs;
    private Animator animator;

    private float footstepRate = 0.3f;
    private float nextStep;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        mainCam = Camera.main;
        audioSrcs = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (alive)
        {
            float xMove = Input.GetAxisRaw("Horizontal");
            float zMove = Input.GetAxisRaw("Vertical");

            // Animation
            if (xMove != 0 || zMove != 0)
            {
                animator.SetInteger("AnimState", 1);

                // Footstep Sounds
                if (Time.time > nextStep)
                {
                    nextStep = Time.time + footstepRate;
                    audioSrcs.volume = 0.3f;
                    audioSrcs.PlayOneShot(sounds[1]);
                }
            }
            else
            {
                animator.SetInteger("AnimState", 0);
            }

            rb.velocity = new Vector2(xMove, zMove) * speed;

            // Fires bullets
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                // Animation for shooting
                animator.SetInteger("AnimState", 2);

                audioSrcs.PlayOneShot(sounds[0]);
                Instantiate(shot, transform.position, transform.rotation);
            }

            // Camera position
            Vector3 camPos = new Vector3(transform.position.x, transform.position.y, -10);
            mainCam.transform.position = camPos;

            // Flip Image
            if (xMove > 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = -2f;

                transform.localScale = scale;
            }
            else if (xMove < 0)
            {
                Vector3 scale = transform.localScale;
                scale.x = +2f;

                transform.localScale = scale;
            }

        }
    }

    public void killPlayer()
    {
        alive = false;
    }
}
