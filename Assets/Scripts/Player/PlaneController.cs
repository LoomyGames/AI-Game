using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{

    private Rigidbody rb;
    public float rotateAmount = 5.0f;
    public float mouseSensitivity = 10f;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float v = Input.GetAxis("Mouse X");
        float h = Input.GetAxis("Mouse Y");

        rb.AddTorque(v * mouseSensitivity, h * mouseSensitivity, 0);

        if(Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.forward);
        }
    }

    void Update()
    {
        if(Input.GetAxis("Horizontal") < 0)
        {
            transform.Rotate(0, 0, rotateAmount);
        }
        else if(Input.GetAxis("Horizontal") > 0)
        {
            transform.Rotate(0, 0, -rotateAmount);
        }
    }
}
