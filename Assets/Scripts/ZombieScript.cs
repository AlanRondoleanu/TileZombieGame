using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float knockback;
    public GameObject player;
    public GameObject prefabZombie;
    public GameObject target;
    public int aggroRange = 3;
    public bool leader = false;
    public int lives = 3;
    public AudioClip[] zombieSounds;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool hit = false;
    private bool chasing = false;
    private ZombieMovement movement;
    private AudioSource audioSrcs;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        movement = GetComponent<ZombieMovement>();
        audioSrcs = GetComponent<AudioSource>();

        InvokeRepeating("zombieSound", 8, 8);
    }

    void Update()
    {
        if (hit)
        {
            Color flash = new Color(0, 0.01f, 0.01f, 0);
            sprite.color += flash;

            if (sprite.color == Color.white)
            {
                hit = false;
            }
        }
        else
        {
            moveTowardsTargets();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Vector3 dir = player.transform.position - transform.position;
            dir = -dir.normalized;

            rb.AddForce(dir * knockback);
            sprite.color = Color.red;
            hit = true;
            lives--;

            // Audio
            zombieSound();

            if (lives <= 0)
            {
                // Kills and passes the leader role
                SchoolManager.instance.PassLeaderRole(gameObject);
                Destroy(gameObject);
            }
        }

        if (collision.gameObject.tag == "Student")
        {
            GameObject zombie = Instantiate(prefabZombie, collision.transform.position, Quaternion.identity);

            zombie.GetComponent<ZombieMovement>().leader = gameObject;
            zombie.GetComponent<ZombieScript>().leader = false;

            SchoolManager.instance.removeStudent(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player" && player.GetComponent<PlayerScript>().alive == true)
        {
            GameObject zombie = Instantiate(prefabZombie, collision.transform.position, Quaternion.identity);

            zombie.GetComponent<ZombieMovement>().leader = gameObject;
            zombie.GetComponent<ZombieScript>().leader = false;

            player.GetComponent<PlayerScript>().killPlayer();
            Destroy(player);
        }
    }

    public void SetTarget(GameObject t_target)
    {
        target = t_target;
        chasing = true;
    }

    void moveTowardsTargets()
    {
        if (target != null)
        {
            movement.setTarget(target.transform.position);

            // Stops chasing if out of range
            if (Vector2.Distance(transform.position, target.transform.position) > aggroRange + 1)
            {
                chasing = false;
                target = null;
                movement.partrolling = true;
            }
        }
    }

    public void setChasing(bool t_active)
    {
        chasing = t_active;
    }

    void zombieSound()
    {
        if (player != null)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);
            const float MAX_DISTANCE = 10.0f;

            if (distance < MAX_DISTANCE)
            {
                float volume = 1.0f - ((distance / MAX_DISTANCE) * 1.0f);

                if (volume > 1.0f)
                {
                    volume = 0;
                }

                // Scale volume
                volume *= 0.5f;

                audioSrcs.volume = volume;
                audioSrcs.PlayOneShot(zombieSounds[Random.Range(0, zombieSounds.Length)]);
            }
        }
    }
}
