using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    float width, height, alive = 0.0f;
    Camera _camera;

    void Start()
    {
        _camera = GetComponent<Camera>();
        height = _camera.orthographicSize;
        width = height * _camera.aspect;
    }

    void Update()
    {
        alive += Time.deltaTime;
        // Ensure all player objects are on screen
        var cameraPosition = transform.position;
        Vector2 min = Vector2.positiveInfinity, max = Vector2.negativeInfinity;
        List<GameObject> objectsToFit = new List<GameObject>();
        objectsToFit.AddRange(GameObject.FindGameObjectsWithTag("Player"));
        objectsToFit.AddRange(GameObject.FindGameObjectsWithTag("Enemy"));

        foreach (var objectToFit in objectsToFit)
        {
            var objectPosition = objectToFit.transform.position;
            if (objectPosition.x < min.x)
                min.x = objectPosition.x;
            if (objectPosition.y < min.y)
                min.y = objectPosition.y;

            if (objectPosition.x > max.x)
                max.x = objectPosition.x;
            if (objectPosition.y > max.y)
                max.y = objectPosition.y;
        }

        min.x = Mathf.Clamp(min.x, -10.0f, -9.0f);
        max.x = Mathf.Clamp(max.x, 10.0f, 15.0f);
        var desiredWidth = max.x - min.x;
        var desiredHeight = max.y - min.y;

        var desiredOrthographicSize = Mathf.Max(desiredHeight, desiredWidth / _camera.aspect);
        _camera.orthographicSize = Mathf.Lerp(_camera.orthographicSize, desiredOrthographicSize, 0.05f);
    }
}
