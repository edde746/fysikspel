using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeScript : ShotBehaviour
{
    public float lifeTime = 2f;
    public float _burstSpeedBoostPercent = 2f;
    bool boosted = false;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (shot) lifeTime -= Time.deltaTime;

        if (lifeTime <= 0 && !boosted)
        {
            rb.velocity *= _burstSpeedBoostPercent;
            boosted = true;
        }
    }
}
