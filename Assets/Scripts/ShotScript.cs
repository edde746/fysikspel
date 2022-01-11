using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base class for all shots, provides on-click functionality
public class ShotBehaviour : MonoBehaviour
{
    public bool clicked = false;
    public bool shot = false;

    private void OnMouseDown()
    {
        clicked = true;
    }

    public void ShotUpdate()
    {
        if (shot && transform.position.y < -6.0f)
            Destroy(gameObject);
    }
}
