using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Move")]
    [SerializeField] private float gravity = -10f;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 10f;
    [SerializeField] private float turnSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float maxSlopeAngle = 45f;
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float deceleration = 5f;

    private CharacterController controller;
    private float velocityY = 0f;
    private float currentHorizontalSpeed = 0f;

    // Footsteps
    [Header("Footsteps")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip walkSound;
    [SerializeField] private AudioClip sprintSound;
    [SerializeField] private float walkStepInterval = 0.5f;
    [SerializeField] private float sprintStepInterval = 0.3f;
    private float stepTimer = 0f;

private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        // smer podla kamery 
        Vector3 forward = Camera.main.transform.forward;
        forward.y = 0;
        forward.Normalize();

        Vector3 right = Camera.main.transform.right;
        right.y = 0;
        right.Normalize();

        // smer pohybu 
        Vector3 moveDir = forward * v + right * h;
        moveDir = PreventSlopeClimb(moveDir);

        if (moveDir.sqrMagnitude > 0.01f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDir);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
        }

        // jump
        HandleJump();

        // gravitacia
        if (controller.isGrounded && velocityY < 0)
        {
            velocityY = -2f;
        }

        velocityY += gravity * Time.deltaTime;

        // acceleracia
        bool isMoving = moveDir.sqrMagnitude > 0.01f;
        float targetSpeed = isMoving ? GetCurrentSpeed() : 0f;
        float smoothedSpeed = SmoothSpeed(targetSpeed, isMoving);

        // horizontalny pohyb
        Vector3 horizontal = moveDir * smoothedSpeed;

        // finalny pohyb
        Vector3 finalMove = new(horizontal.x, velocityY, horizontal.z);
        controller.Move(finalMove * Time.deltaTime);

        // Footsteps
        HandleFootsteps(isMoving);
    }

    private float GetCurrentSpeed()
    {
        if (Input.GetKey(KeyCode.LeftShift))
            return sprintSpeed;

        return walkSpeed;
    }

    private void HandleFootsteps(bool isMoving)
    {
        // Audio nesmie hra? ke? ...
        if(!controller.isGrounded || !isMoving)
        {
            stepTimer = 0f;
            return;
        }

        // Vyber pre walk alebo sprint
        bool isSprinting = currentHorizontalSpeed > (walkSpeed + 0.1f);
        float interval = isSprinting ? sprintStepInterval : walkStepInterval;
        AudioClip clip = isSprinting ? sprintSound : walkSound;

        stepTimer -= Time.deltaTime;
        if(stepTimer <= 0f)
        {
            audioSource.PlayOneShot(clip, 0.5f);
            stepTimer = interval;
        }
    }

    // skok
    private void HandleJump()
    {
        if(controller.isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocityY = jumpForce;
        }
    }

    // zistenie uhla svahu 
    private bool IsOnSteepSlope()
    {
        if(Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit, 2f))
        {
            float slopeAngle = Vector3.Angle(hit.normal, Vector3.up);
            return slopeAngle > maxSlopeAngle;
        }
        return false;
    }

    // uprava pohybu na svahu
    private Vector3 PreventSlopeClimb(Vector3 moveDir)
    {
        if (IsOnSteepSlope())
        {
            // projekcia smeru pohybu tak, aby nezliezal / nesplhal po zvyslom povrchu 
            moveDir = Vector3.ProjectOnPlane(moveDir, Vector3.up);
        }
        return moveDir;
    }

    private float SmoothSpeed(float targetSpeed, bool isMoving)
    {
        if (isMoving)
        {
            // zrychlenie 
            currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, targetSpeed, acceleration * Time.deltaTime);
        }
        else
        {
            // spomalovanie 
            currentHorizontalSpeed = Mathf.MoveTowards(currentHorizontalSpeed, 0f, deceleration * Time.deltaTime);
        }
        return currentHorizontalSpeed;
    }
}
