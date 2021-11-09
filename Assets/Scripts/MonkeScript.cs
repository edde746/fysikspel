using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonkeScript : MonoBehaviour
{
    Rigidbody2D rb;
    public float MaxMagnitude = 20.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    bool Shooting = false;
    Vector3 StartPosition;
    void Update()
    {
        if (!Shooting && Input.GetMouseButtonDown(0))
        {
            Shooting = true;
            StartPosition = transform.position;
        }
        else if (Shooting && Input.GetMouseButtonUp(0))
        {
            // Release the monkey
            var cursorWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var positionDelta = StartPosition - cursorWorldPosition;
            var force = Vector2.ClampMagnitude(new Vector2(positionDelta.x, positionDelta.y) * 5.0f, MaxMagnitude);
            //transform.position = StartPosition - force;
            rb.constraints = RigidbodyConstraints2D.None;
            rb.AddForce(force, ForceMode2D.Impulse);
        }
    }
}
