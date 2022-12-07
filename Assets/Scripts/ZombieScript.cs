using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float knockback;
    public SchoolManager school;
    public GameObject player;
    public GameObject prefabZombie;
    public GameObject target;
    public int aggroRange = 3;
    public bool leader = false;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private bool hit = false;
    private bool chasing = false;
    private ZombieMovement movement;
    
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        movement = GetComponent<ZombieMovement>();
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
            findNearestTarget();
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
        }

        if (collision.gameObject.tag == "Student")
        {
            GameObject zombie = Instantiate(prefabZombie, collision.transform.position, Quaternion.identity);

            zombie.GetComponent<ZombieMovement>().leader = gameObject;
            zombie.GetComponent<ZombieScript>().leader = false;

            school.removeStudent(collision.gameObject);
        }
    }

    void findNearestTarget()
    {
        // Find Student Target if no target is selected
        if (chasing == false)
        {
            foreach (GameObject student in school.allStudents())
            {
                float distance = Vector2.Distance(transform.position, student.transform.position);

                if (distance < aggroRange)
                {
                    target = student;
                    chasing = true;
                    break;
                }
            }
        }

        // Find Player target if one is selected
        if (true)
        {
            float distance = Vector2.Distance(transform.position, player.transform.position);

            if (distance < aggroRange)
            {
                target = player;
                chasing = true;
            }
        }

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
}
