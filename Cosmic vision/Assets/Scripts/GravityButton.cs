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
        Physics.gravity = new Vector3(0, 0.5f, 0);
        isGravity = false;
        player.GetComponent<NoGravityMovement>().GravityChange();
        yield return new WaitForSeconds(5f);
        Physics.gravity = new Vector3(0, -9.81f, 0);
        isGravity = true;
        player.GetComponent<NoGravityMovement>().GravityChange();
        RotateBack();
    }

    private void RotateBack()
    {
        Vector3 rot = new Vector3(
            Mathf.LerpAngle(player.transform.rotation.x, 0f, Time.deltaTime),
            Mathf.LerpAngle(player.transform.rotation.y, 90f, Time.deltaTime),
            Mathf.LerpAngle(player.transform.rotation.z, 0f, Time.deltaTime)
            );
        player.transform.rotation = Quaternion.Euler(rot);
    }
}
