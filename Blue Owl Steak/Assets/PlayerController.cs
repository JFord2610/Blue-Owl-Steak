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

    Vector3 moveVec = Vector3.zero;
    Vector3 rotVec = Vector3.zero;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (disabled) return;

        Vector3 rot = transform.rotation.eulerAngles;
        //lock rotations
        rotVec.z = 0;

        moveVec.x = 0;
        moveVec.z = 0;
        if (Input.GetKey(KeyCode.W))
        {
            moveVec += transform.forward * moveSpeed;
        }
        if (Input.GetAxis("Mouse X") != 0)
        {
            rotVec += new Vector3(0, rot.y + (Input.GetAxis("Mouse X") * mouseSensitivity), 0);
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            rotVec += new Vector3(rot.x + (Input.GetAxis("Mouse Y") * mouseSensitivity), 0, 0);
        }

        transform.rotation = Quaternion.Euler(rotVec);
        cc.Move(moveVec * Time.deltaTime);
    }
}
