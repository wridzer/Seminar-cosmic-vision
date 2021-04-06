using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityButton : MonoBehaviour
{
    private bool inRange;
    public GameObject text, player;
    public bool isGravity = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = true;
            text.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inRange = false;
            text.SetActive(false);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            StartCoroutine(GravityGone());
        }
    }

    private IEnumerator GravityGone()
    {
        Physics.gravity = new Vector3(0, 0.2f, 0);
        isGravity = false;
        player.GetComponent<NoGravityMovement>().GravityChange();
        yield return new WaitForSeconds(10f);
        Physics.gravity = new Vector3(0, -9.81f, 0);
        isGravity = true;
        player.GetComponent<NoGravityMovement>().GravityChange();
        player.GetComponent<PlayerController>().RotateBack();
    }
}
