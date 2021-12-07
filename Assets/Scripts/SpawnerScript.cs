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

    LineRenderer trajectoryRenderer;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        shotPosition = transform.position + slingshotOffset;
        trajectoryRenderer = GameObject.FindGameObjectWithTag("Trajectory").GetComponent<LineRenderer>();

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
        Debug.DrawLine(Vector3.zero, Vector3.down, Color.blue, 1.0f, false);


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
        else if (shooting)
        {
            // Calculate force from cursor
            var cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var positionDelta = shotPosition - cursorWorldPosition;
            var force = Vector2.ClampMagnitude(new Vector2(positionDelta.x, positionDelta.y) * 5.0f, maxMagnitude);

            // Vector2 last = shotPosition;
            List<Vector3> positions = new List<Vector3>();
            for (float i = 0; i < 2; i += 0.1f)
            {
                var pos = positionInTime(i, shotPosition, force);
                positions.Add(pos);
                // Debug.DrawLine(new Vector3(last.x, last.y, transform.position.z), new Vector3(pos.x, pos.y, transform.position.z),Color.blue,1.0f,false);
                // last = pos;
            }
            trajectoryRenderer.enabled = true;
            trajectoryRenderer.positionCount = positions.Count;
            trajectoryRenderer.SetPositions(positions.ToArray());

            if (Input.GetMouseButtonUp(0))
            {
                shooting = false;
                script.shot = true;
                trajectoryRenderer.enabled = false;

                // Release the shot
                // Remove current shot from list
                spawnedShots.RemoveAt(0);

                // Unfreeze shot and release him
                var rb = currentShot.GetComponent<Rigidbody2D>();
                rb.constraints = RigidbodyConstraints2D.None;
                rb.AddForce(force, ForceMode2D.Impulse);
            }
        }
    }

    // Credits: https://stackoverflow.com/questions/61125224/2d-projectile-trajectory-predictionunity-2d
    Vector2 positionInTime(float time, Vector2 initialPosition, Vector2 initialSpeed)
    {
        return initialPosition +
               new Vector2(initialSpeed.x * time, initialSpeed.y * time - (Physics2D.gravity.y / -2) * (time * time));
    }
}
