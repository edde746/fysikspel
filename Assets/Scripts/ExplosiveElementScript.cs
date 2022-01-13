using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosiveElementScript : MonoBehaviour
{
    Vector3 oldPosition;

    public float ActivationTime = 1f;
    public float explosionRadius = 5.0f;
    public float explosionStrength = 14.0f;
    public GameObject explosionParticle;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ActivationTime > 0) ActivationTime -= Time.deltaTime;

        if ((oldPosition != gameObject.transform.position) && ActivationTime <= 0)
        {
            explode();
        }


        oldPosition = gameObject.transform.position;
    }

    void explode()
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
