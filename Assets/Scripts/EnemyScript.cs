using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.rotation.z) >= 0.5f && rb.velocity.magnitude < 0.01f || transform.position.y < -7.0f)
        {
            Die();
        }
    }

    void Die()
    {
        // Add particles or whatever
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            Die();
        }
        else if (
            collision.gameObject.CompareTag("Player") && // Only die when bigger impact velocity
            collision.gameObject.GetComponent<Rigidbody2D>().velocity.magnitude >= rb.velocity.magnitude
        )
        {
            Die();
        }
    }
}
