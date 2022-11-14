using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plane : MonoBehaviour
{
    public int health;

    public float accelerationSpeed;
    public float topSpeed;
    public float turnSpeed;

    Rigidbody2D rb;
    SpriteRenderer renderer;

    public GameObject deathEffect;
    public GameObject hitEffect;

    Shader whiteShader;
    Shader defaultShader;

    public int score;

    protected void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();

        whiteShader = Shader.Find("GUI/Text Shader");
        defaultShader = Shader.Find("Sprites/Default");
    }

    protected void Update()
    {
        if (health <= 0)
        {
            Die();
        }
    }

    public virtual void Die()
    {
        ScoreManager.AddScore(score);
        Instantiate(deathEffect, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }

    public void TakeDamage(int dmg)
    {
        health -= dmg;
        Instantiate(hitEffect, transform.position, Quaternion.identity);
        WhiteSprite();
        StartCoroutine(NormalSprite(0.1f));
    }

    void FixedUpdate()
    {
        Vector2 moveVelocity = transform.up * accelerationSpeed;
        rb.velocity += moveVelocity;

        // Capping Speed
        float curSpeed = rb.velocity.magnitude;


        if (curSpeed > topSpeed)
        {
            float reduction = topSpeed / curSpeed;
            rb.velocity *= reduction;
        }
    }

    void WhiteSprite()
    {
        renderer.material.shader = whiteShader;
        renderer.color = Color.white;
    }

    IEnumerator NormalSprite(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        renderer.material.shader = defaultShader;
        renderer.color = Color.white;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Die();
        }
    }
}
