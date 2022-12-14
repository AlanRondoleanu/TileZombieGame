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

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCam = Camera.main;
        audioSrcs = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (alive)
        {
            float xMove = Input.GetAxisRaw("Horizontal");
            float zMove = Input.GetAxisRaw("Vertical");

            rb.velocity = new Vector2(xMove, zMove) * speed;

            // Fires bullets
            if (Input.GetKey(KeyCode.Mouse0) && Time.time > nextFire)
            {
                nextFire = Time.time + fireRate;

                audioSrcs.PlayOneShot(sounds[0]);
                Instantiate(shot, transform.position, transform.rotation);
            }

            // Camera position
            Vector3 camPos = new Vector3(transform.position.x, transform.position.y, -10);
            mainCam.transform.position = camPos;
        }
    }

    public void killPlayer()
    {
        alive = false;
    }
}
