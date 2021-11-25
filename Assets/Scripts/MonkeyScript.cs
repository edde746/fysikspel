using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeyScript : ShotBehaviour
{
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Explode monkey if he is still;
        if (shot && rb.velocity.magnitude < 0.01f)
        {
            // Add some particles here or something
            Destroy(gameObject);
        }
    }
}
