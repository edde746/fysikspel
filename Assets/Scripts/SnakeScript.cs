using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : ShotBehaviour
{
    public float timeTilBoost = 1.0f;
    public float _burstSpeedBoostPercent = 1.5f;
    bool boosted = false;
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        ShotUpdate();
        if (shot) timeTilBoost -= Time.deltaTime;

        if (timeTilBoost <= 0 && !boosted)
        {
            rb.velocity *= _burstSpeedBoostPercent;
            boosted = true;
        }

        if (shot && rb.velocity.magnitude < 0.01f)
            Destroy(gameObject);
    }
}
