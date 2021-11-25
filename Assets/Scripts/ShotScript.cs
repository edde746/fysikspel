using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehaviour : MonoBehaviour
{
    public bool clicked = false;
    public bool shot = false;

    private void OnMouseDown()
    {
        clicked = true;
    }
}
