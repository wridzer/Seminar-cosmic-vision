using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float movementSpeed;
    public float rotationSpeed = 5f;
    private float walkCal;
    public GameObject player;
    public GameObject head;
    private float xRotation = 0f;
    private float mouseY;
    public float jumpHeigt;
    private bool grounded;

    public float stamina;
    public float staminaRegen = 1;
    public float staminaFull = 5;
    public float sprintSpeed = 10;
    public float tiredSpeed = 2;
    public float normalSpeed = 5;

    public float clampMin = -15;
    public float clampMax = 10;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.y < -100)
        {
            Die();
        }
    }

    private void Move()
    {
        //movement
        movementSpeed = normalSpeed;
        if (stamina >= 0 && Input.GetKey(KeyCode.LeftShift))
        {
            movementSpeed = sprintSpeed;
            stamina -= 1 * Time.deltaTime;
        }
        if (stamina <= staminaFull && Input.GetKey(KeyCode.LeftShift) == false)
        {
            stamina += staminaRegen * Time.deltaTime;
            movementSpeed = tiredSpeed;
        } 
        if (stamina == staminaFull)
        {
            movementSpeed = normalSpeed;
        }

        walkCal = 1 * movementSpeed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
        {
            player.GetComponent<Rigidbody>().transform.Translate(0f, 0f, walkCal, Space.Self);
        }
        if (Input.GetKey(KeyCode.S))
        {
            player.GetComponent<Rigidbody>().transform.Translate(0f, 0f, -walkCal * 0.8f, Space.Self);
        }
        if (Input.GetKey(KeyCode.A))
        {
            player.GetComponent<Rigidbody>().transform.Translate(-walkCal * 0.8f, 0f, 0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.D))
        {
            player.GetComponent<Rigidbody>().transform.Translate(walkCal * 0.8f, 0f, 0f, Space.Self);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Jump();
        }
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }

        //player rotation
        player.GetComponent<Rigidbody>().transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime), 0, Space.World);
        //Head rotation
        mouseY = (-Input.GetAxis("Mouse Y")) * rotationSpeed * Time.deltaTime;
        xRotation = xRotation + mouseY;
        xRotation = Mathf.Clamp(xRotation, clampMin, clampMax);
        Vector3 rotationVector = new Vector3(xRotation, 0f, 0f);
        head.transform.localRotation = Quaternion.Euler(rotationVector);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Terrain")
        {
            grounded = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Terrain")
        {
            grounded = true;
        }
    }

    void Die()
    {
        SceneManager.LoadScene(7);
    }
    void Jump()
    {
        Vector3 jump = new Vector3(0f, jumpHeigt * Time.deltaTime, 0f);
        if (grounded == true)
        {
            player.GetComponent<Rigidbody>().AddRelativeForce(jump);
        }
    }
}
