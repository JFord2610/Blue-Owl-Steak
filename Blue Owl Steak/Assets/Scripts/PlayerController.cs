using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //health
    float _health = 0;
    public float Health
    {
        get
        {
            return _health;
        }
        set
        {
            if (value >= MaxHealth)
                _health = MaxHealth;
            else if (value <= 0)
            {
                Death();
            }
            else
                _health = value;

            HealthChangedEvent?.Invoke(_health, MaxHealth);
        }
    }
    [SerializeField] float MaxHealth = 100.0f;

    //variables
    [Header("General")]
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float gravityStrength = 3.0f;
    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;
    public float swordDamage = 50.0f;

    public bool dead = false;
    public bool disabled = false;

    [HideInInspector] public GameObject objectBeingHeld = null;
    Rigidbody heldObjectRB = null;
    bool holdingObject = false;

    //references
    [SerializeField] GameObject holdingPoint = null;
    CharacterController cc = null;
    Camera cam = null;
    Animator anim = null;
    [SerializeField] BoxCollider swordCol = null;

    Vector3 startPos = Vector3.zero;
    Vector3 moveVec = Vector3.zero;
    float xRot = 0.0f;
    [SerializeField] float yRot = 0.0f;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;

        Health = MaxHealth;
    }

    private void Update()
    {
        if (disabled || dead) return;

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
        if (holdingObject && objectBeingHeld != null)
        {
            //if (Vector3.Distance(holdingPoint.transform.position, objectBeingHeld.transform.position) >= 0.01f)
            //    objectBeingHeld.transform.position = Vector3.Lerp(objectBeingHeld.transform.position, holdingPoint.transform.position, Time.deltaTime);
            if (Input.GetKeyDown(KeyCode.F))
            {
                EventManager.InvokePlayerDroppedItem();
                holdingObject = false;
                objectBeingHeld.transform.parent = transform.parent;
                heldObjectRB = objectBeingHeld.GetComponent<Rigidbody>();
                heldObjectRB.useGravity = true;
                heldObjectRB.velocity = cc.velocity;
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            RaycastHit hit;
            //Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red, 2.0f);
            if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, 2.0f, LayerMask.GetMask("Interactable")))
            {
                hit.transform.parent = holdingPoint.transform;
                hit.transform.GetComponent<Rigidbody>().useGravity = false;
                objectBeingHeld = hit.transform.gameObject;
                holdingObject = true;
            }
        }
        #endregion

        #region combat
        if (Input.GetMouseButtonDown(0) && !holdingObject)
        {
            anim.SetTrigger("Attack");
        }
        #endregion

        anim.SetFloat("MoveSpeed", cc.velocity.magnitude);
        cc.Move(moveVec * Time.deltaTime);
    }

    void Death()
    {
        dead = true;
        DeathEvent?.Invoke();
    }

    public void TakeDamage(float damage)
    {
        if (dead) return;
        Health -= damage;
    }

    public void EnableSwordCollider()
    {
        swordCol.enabled = false;
    }
    public void DisableSwordCollider()
    {
        swordCol.enabled = true;
    }

    public delegate void HealthHandler(float _health, float _maxHealth);
    public static HealthHandler HealthChangedEvent;

    public delegate void EventHandler();
    public static EventHandler DeathEvent;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        Rigidbody rb = hit.collider.attachedRigidbody;
        if (rb == null || rb.isKinematic || hit.moveDirection.y < -0.3f) return;
        Vector3 dir = new Vector3(hit.moveDirection.x, 0, hit.moveDirection.z);
        rb.velocity = dir * 2.0f;
    }
}
