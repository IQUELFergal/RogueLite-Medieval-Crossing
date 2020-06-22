using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Rotation")]
    public RotationMode rotationMode = RotationMode.followMouse;
    public enum RotationMode { none, followMouse, controlledWithInputs };

    [Header("Speed")]
    public float moveSpeed = 6;
    Vector2 lastInputDir=Vector2.down;

    [Header("Smoothing")]
    public bool instantStop = true;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float speedSmoothTime = 0.1f;
    float speedSmoothVelocity;

    [Header("Debug")]
    [SerializeField] float currentSpeed;

    Rigidbody2D playerRigidbody;
    SpriteRenderer spriteRenderer;
    Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerRigidbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        HandleMovement();

        
    }

    void HandleMovement()
    {
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;
        if (inputDir != Vector2.zero) lastInputDir = inputDir;

        //Rotation
        if (rotationMode == RotationMode.none)
        {
            transform.eulerAngles = Vector3.zero;
        }
        else
        {
            if (rotationMode == RotationMode.controlledWithInputs)
            {
                if (inputDir != Vector2.zero)
                {
                    float targetRotation = Mathf.Atan2(-inputDir.x, inputDir.y) * Mathf.Rad2Deg;
                    transform.eulerAngles = Vector3.forward * Mathf.SmoothDampAngle(transform.eulerAngles.z, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
                }
            }
            else
            {
                if (rotationMode == RotationMode.followMouse)
                {
                    Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    Vector2 lookDir = MousePos - playerRigidbody.position;
                    float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
                    playerRigidbody.rotation = angle;
                }
            }
        }

        //Position
        float targetSpeed = moveSpeed * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        playerRigidbody.velocity = currentSpeed * (instantStop ? inputDir : lastInputDir);
        currentSpeed = playerRigidbody.velocity.magnitude;

        //Animation
        float animationSpeedPercent = currentSpeed / moveSpeed * inputDir.magnitude;
        animator.SetFloat("Speed", animationSpeedPercent, speedSmoothTime, Time.deltaTime);

        if(lastInputDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else spriteRenderer.flipX = true;
        animator.SetFloat("DirX", lastInputDir.x);
        animator.SetFloat("DirY", lastInputDir.y);

    }

    private void OnValidate()
    {
        if (rotationMode == RotationMode.none)
        {
            playerRigidbody = GetComponent<Rigidbody2D>();
            playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            if (rotationMode == RotationMode.controlledWithInputs)
            {
                playerRigidbody = GetComponent<Rigidbody2D>();
                playerRigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else
            {
                if (rotationMode == RotationMode.followMouse)
                {
                    playerRigidbody = GetComponent<Rigidbody2D>();
                    playerRigidbody.constraints = RigidbodyConstraints2D.None;
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
            }
        }
    }

    public Vector2 GetLastInputDir()
    {
        return lastInputDir;
    }
}
