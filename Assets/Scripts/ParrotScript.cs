using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotScript : ShotBehaviour
{
    public float lifetime = 2.0f;
    public float explosionRadius = 5.0f;
    public float explosionStrength = 14.0f;
    public GameObject explosionParticle;

    void Start()
    {

    }

    void Update()
    {
        if (shot) lifetime -= Time.deltaTime;

        if (lifetime <= 0.0f)
        {
            Instantiate(explosionParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

            var explosionTargets = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
            foreach (var target in explosionTargets)
            {
                if (!target.attachedRigidbody || target.gameObject == gameObject) continue;

                Vector2 positionDelta = target.transform.position - transform.position;
                var force = positionDelta.normalized * explosionStrength / positionDelta.sqrMagnitude;

                target.attachedRigidbody.AddForce(force, ForceMode2D.Impulse);
            }
            Destroy(gameObject);
        }
    }
}
