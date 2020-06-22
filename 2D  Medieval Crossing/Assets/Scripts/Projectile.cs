using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    float damage = 1;
    public float speed = 4;

    public ParticleSystem trail;
    public ParticleSystem hitParticle;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed + (Vector3)FindObjectOfType<PlayerController>().GetComponent<Rigidbody2D>().velocity/3;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.name + " hit for " + damage + " damage");
        if (collision.GetComponent<Projectile>() == null)
        {
            Destroy(gameObject);
        }
       
    }
}
