using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityButton : MonoBehaviour
{
    private bool inRange;
    public GameObject text;

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
            Debug.Log("wtf bro");
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
    }

    private IEnumerator GravityGone()
    {
        Physics.gravity = new Vector3(0, 0.5f, 0);
        Debug.Log("doe vlieg");
        yield return new WaitForSeconds(5f);
    }
}
