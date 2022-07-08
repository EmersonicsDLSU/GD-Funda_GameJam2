using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseNavigate : MonoBehaviour
{
    //mouse coordinates
    private float mouseX;
    private float mouseY;

    [SerializeField] private float mouseSensitivity = 100.0f;

    [SerializeField] private Transform playerBody;

    private float xRotation;

    // Start is called before the first frame update
    void Start()
    {
        //turns off the cursor display
        Cursor.lockState = CursorLockMode.Locked;
        //turns on
        //Cursor.lockState = CursorLockMode.Confined;
    }

    // Update is called once per frame
    void Update()
    {
        //gets the delta coordinates of the mouse cursor
        mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90.0f, 90.0f);

        //rotates the camera in the pitch-axis
        transform.localRotation = Quaternion.Euler(xRotation, 0.0f, 0.0f);
        //rotates the player in the yaw-axis
        playerBody.Rotate(Vector3.up, mouseX);

    }
}
