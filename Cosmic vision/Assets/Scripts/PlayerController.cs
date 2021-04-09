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

    public float speed = 40f;
    public bool rotateBitch;

    [SerializeField] private float grabRange = 5;
    [SerializeField] private bool hasItem;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        hasItem = false;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.y < -100)
        {
            Die();
        }
        if (rotateBitch)
        {
            this.transform.rotation = Quaternion.RotateTowards(this.transform.rotation, Quaternion.Euler(0, 90, 0), speed * Time.deltaTime);
            if(transform.rotation == Quaternion.Euler(0, 90, 0))
            {
                rotateBitch = false;
            }
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
        /*if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene("SampleScene");
        }*/
        if (Input.GetMouseButtonDown(0))
        {
            itemPickup();
        }

        //player rotation
        player.GetComponent<Rigidbody>().transform.Rotate(0, (Input.GetAxis("Mouse X") * rotationSpeed), 0, Space.World);
        //Head rotation
        mouseY = (-Input.GetAxis("Mouse Y")) * rotationSpeed;
        xRotation = xRotation + mouseY;
        xRotation = Mathf.Clamp(xRotation, clampMin, clampMax);
        Vector3 rotationVector = new Vector3(xRotation, 0f, 0f);
        head.transform.localRotation = Quaternion.Euler(rotationVector);
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Ground")
        {
            grounded = false;
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Ground")
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

    public void RotateBack()
    {
        rotateBitch = true;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void itemPickup()
    {
        if (!hasItem)
        {
            RaycastHit hit;
            if (Physics.Raycast(head.transform.position, transform.TransformDirection(Vector3.forward), out hit, grabRange))
            {
                if (hit.collider.tag == "Prop")
                {
                    hit.collider.transform.parent = gameObject.transform;
                    hasItem = true;
                }
            }
        } else {
            foreach (Transform child in transform)
            {
                if (child.tag == "Prop")
                {
                    child.transform.parent = null;
                    hasItem = false;
                }
            }
        }
    }
}
