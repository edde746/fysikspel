using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParrotScript : ShotBehaviour
{
    public float lifetime = 2.0f;
    public float explosionRadius = 5.0f;
    public float explosionStrength = 20.0f;
    public GameObject explosionParticle;

    void Update()
    {
        ShotUpdate();
        if (shot) lifetime -= Time.deltaTime;

        if (lifetime <= 0.0f)
        {
            // Create explosion particle emitter, this will delete itself once played
            Instantiate(explosionParticle, transform.position, Quaternion.identity).GetComponent<ParticleSystem>().Play();

            // Fetch targets within range and apply force if the target is valid
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
