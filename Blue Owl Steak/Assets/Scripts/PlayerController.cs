using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //variables
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float gravityStrength = 3.0f;
    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;

    public bool disabled = false;

    [HideInInspector] public GameObject objectBeingHeld = null;
    Rigidbody heldObjectRB = null;
    bool holdingObject = false;

    //references
    [SerializeField] GameObject holdingPoint = null;
    CharacterController cc = null;
    Camera cam = null;

    Vector3 moveVec = Vector3.zero;
    float xRot = 0.0f;
    [SerializeField] float yRot = 0.0f;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (disabled) return;

        #region movement
        moveVec.x = 0;
        moveVec.z = 0;
        if (cc.isGrounded)
            moveVec.y = 0;
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
        if (Input.GetKey(KeyCode.Space) && cc.isGrounded)
        {
            moveVec.y = jumpSpeed;
        }

        //apply gravity
        moveVec.y -= gravityStrength * Time.deltaTime;

        #endregion

        #region camera
        xRot += Input.GetAxis("Mouse Y") * mouseSensitivity;
        yRot += Input.GetAxis("Mouse X") * mouseSensitivity;

        xRot = Mathf.Clamp(xRot, -90.0f, 90.0f);

        transform.rotation = Quaternion.Euler(0, yRot, 0);
        cam.transform.rotation = Quaternion.Euler(new Vector3(-xRot, 0, 0) + transform.rotation.eulerAngles);
        #endregion

        #region interacting
        if (holdingObject)
        {
            if (Vector3.Distance(holdingPoint.transform.position, objectBeingHeld.transform.position) >= 0.01f)
                objectBeingHeld.transform.position = Vector3.Lerp(objectBeingHeld.transform.position, holdingPoint.transform.position, Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.F))
            {
                holdingObject = false;
                objectBeingHeld.transform.parent = transform.parent;
                heldObjectRB = objectBeingHeld.GetComponent<Rigidbody>();
                heldObjectRB.isKinematic = false;
                heldObjectRB.useGravity = true;
                heldObjectRB.velocity = cc.velocity;
                EventManager.InvokePlayerDroppedItem();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red, 2.0f);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2.0f, LayerMask.GetMask("Interactable")))
            {
                hit.transform.parent = holdingPoint.transform;
                hit.transform.rotation = Quaternion.Euler(0, cam.transform.rotation.y, 0);
                hit.transform.GetComponent<Rigidbody>().isKinematic = true;
                hit.transform.GetComponent<Rigidbody>().useGravity = false;
                objectBeingHeld = hit.transform.gameObject;
                holdingObject = true;
            }
        }
        #endregion

        cc.Move(moveVec * Time.deltaTime);
    }

    IEnumerator LerpRotation(Transform obj)
    {
        while (Vector3.Distance(cam.transform.rotation.eulerAngles, obj.rotation.eulerAngles) >= 0.01f)
        {
            obj.rotation = Quaternion.Euler(Vector3.Lerp(obj.rotation.eulerAngles, cam.transform.rotation.eulerAngles, Time.deltaTime));
            yield return new WaitForFixedUpdate();
        }
    }
}
