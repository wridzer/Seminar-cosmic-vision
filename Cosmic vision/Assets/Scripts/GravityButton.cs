using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GravityButton : MonoBehaviour
{
    private bool inRange;
    public GameObject text, player;
    public bool isGravity = true;

    private Vector3 rot;
    public Vector3 targetAngle = new Vector3(0f, 90f, 0f);

    private void Start()
    {
        //rot = transform.eulerAngles;
    }

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
        float Lrotatex = Mathf.MoveTowardsAngle(transform.eulerAngles.x, targetAngle.x, 1 * Time.deltaTime);
        float Lrotatey = Mathf.MoveTowardsAngle(transform.eulerAngles.y, targetAngle.y, 1 * Time.deltaTime);
        float Lrotatez = Mathf.MoveTowardsAngle(transform.eulerAngles.z, targetAngle.z, 1 * Time.deltaTime);
        //rot = new Vector3(Lrotatex, Lrotatey, Lrotatez);
        player.transform.eulerAngles = new Vector3(Lrotatex, Lrotatey, Lrotatez);
    }
}
