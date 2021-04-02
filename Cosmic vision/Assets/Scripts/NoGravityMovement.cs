using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoGravityMovement : MonoBehaviour
{

    public GameObject gravityButton;
    private bool gravatating = true;

    private void Start()
    {
        //gravatating = gravityButton.GetComponent<GravityButton>().isGravity;
    }

    private void Update()
    {
        if (gravatating)
        {
            GetComponent<PlayerController>().enabled = true;
            GetComponent<UnityTemplateProjects.SimpleCameraController>().enabled = false;
        }
        if (!gravatating)
        {
            GetComponent<PlayerController>().enabled = false;
            GetComponent<UnityTemplateProjects.SimpleCameraController>().enabled = true;
        }
    }

    public void GravityChange()
    {
        gravatating = !gravatating;
    }
}
