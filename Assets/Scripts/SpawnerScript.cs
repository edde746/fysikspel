using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float shotGap = 0.02f;
    public float maxMagnitude = 20.0f;
    public Vector3 slingshotOffset;
    private Vector3 shotPosition;
    public List<GameObject> queuedShots;
    [HideInInspector]
    public List<GameObject> spawnedShots;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        shotPosition = transform.position + slingshotOffset;

        var spawnPosition = transform.position;
        queuedShots.ForEach(shot =>
        {
            var newShot = Instantiate(shot, spawnPosition, Quaternion.identity);

            // Get width of Shot for line up
            var width = 0.0f;
            var collider = newShot.GetComponent<CircleCollider2D>();
            if (collider)
            {
                width = collider.radius;
            }
            else
            {
                var boxCollider = newShot.GetComponent<BoxCollider2D>();
                width = boxCollider.size.x;
            }

            spawnPosition = new Vector3(spawnPosition.x - width - shotGap, spawnPosition.y, spawnPosition.z);
            spawnedShots.Add(newShot);
        });
    }

    bool shooting = false;
    void Update()
    {
        // Check if we have a shot to shoot
        if (spawnedShots.Count == 0) return;

        // Fetch shot from queue and set to slingshot position
        var currentShot = spawnedShots[0];
        currentShot.transform.position = shotPosition;

        // Inefficient fetching script each frame, add a cache?
        var script = currentShot.GetComponent<ShotBehaviour>();
        if (!shooting && script.clicked)
        {
            shooting = true;
        }
        else if (shooting && Input.GetMouseButtonUp(0))
        {
            shooting = false;
            script.shot = true;

            // Release the shot
            // Remove current shot from list
            spawnedShots.RemoveAt(0);

            // Calculate force from cursor
            var cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var positionDelta = shotPosition - cursorWorldPosition;
            var force = Vector2.ClampMagnitude(new Vector2(positionDelta.x, positionDelta.y) * 5.0f, maxMagnitude);

            // Unfreeze shot and release him
            var rb = currentShot.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
