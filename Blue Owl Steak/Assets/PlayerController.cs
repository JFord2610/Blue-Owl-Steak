using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float gravityStrength = 3.0f;
    [SerializeField] float mouseSensitivity = 1.0f;
    public bool disabled = false;
    CharacterController cc = null;
    Camera cam = null;

    Vector3 moveVec = Vector3.zero;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (disabled) return;

        //wasd movement
        moveVec.x = 0;
        moveVec.z = 0;
        if (Input.GetKey(KeyCode.W))
        {
            moveVec += transform.forward * moveSpeed;
        }
        if(Input.GetKey(KeyCode.S))
        {
            moveVec -= transform.forward * moveSpeed;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveVec -= transform.right * moveSpeed;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveVec += transform.right * moveSpeed;
        }

        //camera movement
        if (Input.GetAxis("Mouse X") != 0)
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + new Vector3(0, transform.rotation.y + (Input.GetAxis("Mouse X") * mouseSensitivity), 0));
        if (Input.GetAxis("Mouse Y") != 0)
            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles + new Vector3(cam.transform.rotation.x - (Input.GetAxis("Mouse Y") * mouseSensitivity), 0, 0));
        
        cc.Move(moveVec * Time.deltaTime);
    }
}
