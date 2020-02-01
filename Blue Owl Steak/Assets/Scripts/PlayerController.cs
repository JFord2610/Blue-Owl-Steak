using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float gravityStrength = 3.0f;
    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] GameObject holdingPoint = null;
    public bool disabled = false;
    CharacterController cc = null;
    Camera cam = null;

    bool holdingObject = false;
    GameObject objectBeingHeld = null;

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
        if (Input.GetKey(KeyCode.S))
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
        {
            Vector3 vec = new Vector3(0, transform.rotation.y + (Input.GetAxis("Mouse X") * mouseSensitivity), 0);
            transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + vec);
        }
        if (Input.GetAxis("Mouse Y") != 0)
        {
            Vector3 vec = new Vector3(cam.transform.rotation.x - (Input.GetAxis("Mouse Y") * mouseSensitivity), 0, 0);
            cam.transform.rotation = Quaternion.Euler(cam.transform.rotation.eulerAngles + vec);
        }

        if(holdingObject)
        {
            if(Vector3.Distance(holdingPoint.transform.position, objectBeingHeld.transform.position) >= 0.01f)
                objectBeingHeld.transform.position = Vector3.Lerp(objectBeingHeld.transform.position, holdingPoint.transform.position, Time.deltaTime);
            if(Input.GetKeyDown(KeyCode.F))
            {
                holdingObject = false;
                objectBeingHeld.transform.parent = transform.parent;
                objectBeingHeld.transform.GetComponent<Rigidbody>().useGravity = true;
            }
        }
        else if(Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red, 2.0f);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2.0f, LayerMask.GetMask("Interactable")))
            {
                hit.transform.parent = holdingPoint.transform;
                hit.transform.GetComponent<Rigidbody>().useGravity = false;
                objectBeingHeld = hit.transform.gameObject;
                holdingObject = true;
            }
        }
        cc.Move(moveVec * Time.deltaTime);
    }

    IEnumerator LerpRotation(Transform obj)
    {
        while(Vector3.Distance(cam.transform.rotation.eulerAngles, obj.rotation.eulerAngles) >= 0.01f)
        {
            obj.rotation = Quaternion.Euler(Vector3.Lerp(obj.rotation.eulerAngles, cam.transform.rotation.eulerAngles, Time.deltaTime));
            yield return new WaitForFixedUpdate();
        }
    }
}
