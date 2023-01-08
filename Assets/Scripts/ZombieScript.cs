using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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
    public AudioClip footstep;
    public bool hit = false;

    private Rigidbody2D rb;
    private ZombieMovement movement;
    private AudioSource audioSrcs;
    private SpriteRenderer[] renderers;
    private UnityEngine.AI.NavMeshAgent agent;

    private float footstepRate = 0.3f;
    private float nextStep;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderers = GetComponentsInChildren<SpriteRenderer>();
        movement = GetComponent<ZombieMovement>();
        audioSrcs = GetComponent<AudioSource>();
        agent = GetComponent<NavMeshAgent>();

        audioSrcs.volume = 0.5f;

        InvokeRepeating("zombieSound", 8, 8);
    }

    void Update()
    {
        if (hit)
        {
            foreach (SpriteRenderer renderer in renderers)
            {
                Color flash = new Color(0, 0.01f, 0.01f, 0);
                renderer.color += flash;

                if (renderer.color == Color.white)
                {
                    hit = false;
                }
            }
        }
        else
        {
            moveTowardsTargets();
        }

        setVolumeDistance();

        // Footstep Sounds
        Vector3 moveDirection = agent.velocity;
        if (Time.time > nextStep && moveDirection != Vector3.zero)
        {
            nextStep = Time.time + footstepRate;
            audioSrcs.PlayOneShot(footstep);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            // Took Damage
            hit = true;
            lives--;

            // Pushback
            Vector3 dir = player.transform.position - transform.position;
            dir = -dir.normalized;
            rb.AddForce(dir * knockback);

            // Highlight Damage
            foreach (SpriteRenderer renderer in renderers)
            {
                renderer.color = Color.red;
            }
   
            // Audio
            zombieSound();

            // Death event
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
    }

    void moveTowardsTargets()
    {
        if (target != null)
        {
            movement.setTarget(target.transform.position);

            // Stops chasing if out of range
            if (Vector2.Distance(transform.position, target.transform.position) > aggroRange + 1)
            {
                target = null;
                movement.partrolling = true;
            }
        }
    }

    void zombieSound()
    {
        audioSrcs.PlayOneShot(zombieSounds[Random.Range(0, zombieSounds.Length)]);
    }

    void setVolumeDistance()
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
                volume *= 0.3f;

                audioSrcs.volume = volume;
            }
            else
            {
                audioSrcs.volume = 0;
            }
        }
        else
        {
            audioSrcs.volume = 0;
        }
    }
}
