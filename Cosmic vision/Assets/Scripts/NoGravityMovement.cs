using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGravityMovement : MonoBehaviour
{

    public GameObject gravityButton;
    private bool gravatating = true;

    private void Update()
    {
        if (gravatating)
        {
            GetComponent<PlayerController>().enabled = true;
            GetComponent<SimpleCameraController>().enabled = false;
        }
        if (!gravatating)
        {
            GetComponent<PlayerController>().enabled = false;
            GetComponent<SimpleCameraController>().enabled = true;
        }
    }

    public void GravityChange()
    {
        gravatating = !gravatating;
    }
}
