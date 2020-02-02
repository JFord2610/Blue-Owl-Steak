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
    public float MaxHealth = 100.0f;

    //variables
    [Header("General")]
    [SerializeField] float moveSpeed = 10.0f;
    [SerializeField] float gravityStrength = 3.0f;
    [SerializeField] float mouseSensitivity = 1.0f;
    [SerializeField] float jumpSpeed = 10.0f;
    public float swordDamage = 50.0f;

    [SerializeField] float grabRange = 2.0f;
    [SerializeField] float letGoRange = 2.5f;

    [HideInInspector] public bool dead = false;
    [HideInInspector] public bool disabled = false;

    [HideInInspector] public GameObject objectBeingHeld = null;
    Rigidbody heldObjectRB = null;
    bool holdingObject = false;

    //references
    [SerializeField] GameObject holdingPoint = null;
    CharacterController cc = null;
    Camera cam = null;
    Animator anim = null;
    [SerializeField] BoxCollider swordCol = null;
    [SerializeField] GameObject sword = null;

    Vector3 startPos = Vector3.zero;
    Vector3 moveVec = Vector3.zero;
    Vector3 smoothDampVel = Vector3.zero;
    float xRot = 0.0f;
    float yRot = 0.0f;
    private void Start()
    {
        cc = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
        RaycastHit hit;
        bool hasHit = Physics.SphereCast(cam.transform.position, 0.75f, cam.transform.forward, out hit, grabRange, LayerMask.GetMask("Interactable"));

        if(hasHit && !holdingObject)
            GameManager.instance.uiManager.canInteract = true;
        else
            GameManager.instance.uiManager.canInteract = false;

        if (objectBeingHeld == null)
        {
            holdingObject = false;
            sword.SetActive(true);
        }
        if (holdingObject && objectBeingHeld != null)
        {
            float distToObject = Vector3.Distance(holdingPoint.transform.position, objectBeingHeld.transform.position);
            if (distToObject >= 0.01f)
            {
                objectBeingHeld.transform.position = Vector3.SmoothDamp(objectBeingHeld.transform.position, holdingPoint.transform.position, ref smoothDampVel, 0.3f);
                if (heldObjectRB.velocity.magnitude >= 1.0f)
                    heldObjectRB.velocity *= 0.5f;
            }
            else if (distToObject >= letGoRange)
            {
                DropHeldObject();
            }
            if (Input.GetKeyDown(KeyCode.F))
            {
                DropHeldObject();
            }
        }
        else if (Input.GetKeyDown(KeyCode.F))
        {
            //Debug.DrawRay(cam.transform.position, cam.transform.forward, Color.red, 2.0f);
            if (hasHit)
            {
                hit.transform.parent = holdingPoint.transform;
                objectBeingHeld = hit.transform.gameObject;
                heldObjectRB = objectBeingHeld.GetComponent<Rigidbody>();
                heldObjectRB.useGravity = false;
                holdingObject = true;
                sword.SetActive(false);

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

    public void DropHeldObject()
    {
        EventManager.InvokePlayerDroppedItem();
        objectBeingHeld.transform.parent = transform.parent;
        heldObjectRB = objectBeingHeld.GetComponent<Rigidbody>();
        heldObjectRB.velocity = cc.velocity;
        heldObjectRB.useGravity = true;
        holdingObject = false;
        objectBeingHeld = null;
        sword.SetActive(true);
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
