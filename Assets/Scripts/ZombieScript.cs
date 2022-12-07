using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieScript : MonoBehaviour
{
    public float knockback;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    bool hit = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (hit)
        {
            Color flash = new Color(0, 0.01f, 0.01f, 0);
            sprite.color += flash;

            if (sprite.color.b == 0)
            {
                hit = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Vector3 dir = collision.transform.position - transform.position;
            dir = -dir.normalized;

            rb.AddForce(dir * knockback);
            sprite.color = Color.red;
            hit = true;
        }
    }
}
