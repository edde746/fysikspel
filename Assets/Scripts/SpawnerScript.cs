using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    public float monkeyGap = 0.02f;
    public float maxMagnitude = 20.0f;
    public Vector3 slingshotOffset;
    private Vector3 monkeyPosition;
    public List<GameObject> queuedMonkeys;
    [HideInInspector]
    public List<GameObject> spawnedMonkeys;
    void Start()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        monkeyPosition = transform.position + slingshotOffset;

        var spawnPosition = transform.position;
        queuedMonkeys.ForEach(monkey =>
        {
            var newMonkey = Instantiate(monkey, spawnPosition, Quaternion.identity);

            // Get width of Monkey for line up
            var width = 0.0f;
            var collider = newMonkey.GetComponent<CircleCollider2D>();
            if (collider)
            {
                width = collider.radius;
            }
            else
            {
                var boxCollider = newMonkey.GetComponent<BoxCollider2D>();
                width = boxCollider.size.x;
            }

            spawnPosition = new Vector3(spawnPosition.x - width - monkeyGap, spawnPosition.y, spawnPosition.z);
            spawnedMonkeys.Add(newMonkey);
        });
    }

    bool shooting = false;
    void Update()
    {
        // Check if we have a monkey to shoot
        if (spawnedMonkeys.Count == 0) return;

        // Fetch monkey from queue and set to slingshot position
        var currentMonkey = spawnedMonkeys[0];
        currentMonkey.transform.position = monkeyPosition;

        // Inefficient fetching script each frame, add a cache?
        var monkeyScript = currentMonkey.GetComponent<MonkeyScript>();
        if (!shooting && monkeyScript.clicked)
        {
            shooting = true;
        }
        else if (shooting && Input.GetMouseButtonUp(0))
        {
            shooting = false;
            monkeyScript.shot = true;

            // Release the monkey
            // Remove current monkey from list
            spawnedMonkeys.RemoveAt(0);

            // Calculate force from cursor
            var cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var positionDelta = monkeyPosition - cursorWorldPosition;
            var force = Vector2.ClampMagnitude(new Vector2(positionDelta.x, positionDelta.y) * 5.0f, maxMagnitude);

            // Unfreeze monkey and release him
            var rb = currentMonkey.GetComponent<Rigidbody2D>();
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
