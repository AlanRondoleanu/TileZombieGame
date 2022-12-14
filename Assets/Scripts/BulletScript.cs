using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public float speed;

    private GameObject player;
    private Camera mainCam;

    void Start()
    {
        mainCam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        player = GameObject.FindGameObjectWithTag("Player");

        Vector3 mousePosition = mainCam.ScreenToWorldPoint(Input.mousePosition);
        Vector3 direction = mousePosition - player.transform.position;
        Vector3 rotation = player.transform.position  - mousePosition;
        float rotZ = Mathf.Atan2(rotation.y, rotation.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0,0,rotZ);
        GetComponent<Rigidbody2D>().velocity = new Vector3(direction.x, direction.y).normalized * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}

