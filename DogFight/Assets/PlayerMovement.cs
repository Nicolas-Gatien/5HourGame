using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;

    public float maxSpeed = 20;
    public float accelerationSpeed = 5;
    public float turnSpeed;

    public float dashSpeed = 20;

    Vector2 movement;

    [HideInInspector]
    public float curSpeed;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Vector2 dashVelocity = transform.up * dashSpeed;
            rb.velocity = dashVelocity;
        }
        movement = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxis("Vertical")); // Getting Input
    }

    void FixedUpdate()
    {
        // Moving The Ship
        Vector2 moveVelocity = transform.up * accelerationSpeed * Mathf.Abs(movement.y);    
        rb.velocity += moveVelocity;



        // Turning the Ship
        transform.Rotate(0, 0, -(movement.x * turnSpeed) * Time.fixedDeltaTime);

        // Capping Speed
        curSpeed = rb.velocity.magnitude;


        if (curSpeed > maxSpeed)
        {
            float reduction = maxSpeed / curSpeed;
            rb.velocity *= reduction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy") || collision.CompareTag("Missile"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
        }
    }
}