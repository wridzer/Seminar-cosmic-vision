using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPress : MonoBehaviour
{
    private bool inRange;
    public GameObject text, door;


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

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && inRange)
        {
            if (inRange)
            {
                door.GetComponent<Animator>().Play("open");
                GetComponent<Animator>().SetBool("Pressed", true);
                StartCoroutine(AnimStop());
                //door.GetComponent<Transform>().rotation = Quaternion.Euler(0, -90, 0);
            }
            Debug.Log("door open, animation play");
        }
    }

    private IEnumerator AnimStop()
    {
        yield return new WaitForSeconds(0.1f);
        GetComponent<Animator>().SetBool("Pressed", false);
    }
}
