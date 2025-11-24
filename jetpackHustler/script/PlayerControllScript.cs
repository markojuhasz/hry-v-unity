using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerControllScript : MonoBehaviour
{
    private PlayerActions input;
    private Vector2 moveInput;
    private Rigidbody2D rb;
    private Vector3 startPos;
    private Vector3 savePos;
    private GameManager gameManager;
    private Vector3 playerScale;
    private PlayerAudioScript audioScript;

    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 10f;
    [SerializeField] private float acceleration = 10f;
    [SerializeField] private float decelleration = 10f;
    [SerializeField] private float maxSpeed = 10f;

    [Header("Tilt Settings")]
    [SerializeField] private float maxTilt = 15f;
    [SerializeField] private float tiltSpeed = 5f;

    [Header("Jump Settings")]
    [SerializeField] private float coyoteTime = 0.20f;
    private float coyoteTimeCounter; 
    [SerializeField] private float jumpVelocity = 7f;
    [SerializeField] private float jumpHoldVelocity = 3f;
    [SerializeField] private float maxHoldTime = 0.50f;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundCheckRadius = 0.1f;

    [Header("Jetpack Settings")]
    [SerializeField] private float jetPackForce = 10f;
    [SerializeField] private float jetPackMaxY = 10f;
    private float groundY;
    public bool hasJetpack = false;
    [SerializeField] private GameObject motorStart; // Motor == sprite rakety jetpacku

    private bool isJumping;

    private float jumpHoldTimer;

    private void Start()
    {
        playerScale = transform.localScale;
        startPos = transform.position;
        groundY = transform.position.y;

        audioScript = GetComponent<PlayerAudioScript>();
        gameManager = FindAnyObjectByType<GameManager>();
    }

    private void Awake()
    {
        input = new PlayerActions();
        rb = GetComponent<Rigidbody2D>();
    }

    // Zapnem / Vypnem Input
    private void OnEnable() => input.Enable();
    private void OnDisable() => input.Disable();


    private void Update()
    {
        // Move input cez inputManager a vector2
        moveInput = input.Movement.Move.ReadValue<Vector2>();
        
        // Zapni zvuk chodze pri stlaceni chodze
        if (input.Movement.Move.WasPressedThisFrame() && isGrounded())
        {
            audioScript.AudioSource.PlayOneShot(audioScript.walkSound);
        }

        // coyote time
        if (isGrounded())
        {
            coyoteTimeCounter = coyoteTime; //reset na zemi
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; //odpocet vo vzduchu
        }

        // Zaciatok skoku, pridanie zvuku 
        if (input.Movement.Jump.WasPerformedThisFrame() && isGrounded() && coyoteTimeCounter > 0f)
        {
            StartJump();
            coyoteTimeCounter = 0f; //spotrebuje coyoteTimeCounter
            audioScript.AudioSource.PlayOneShot(audioScript.jumpSound);
        }

        // Drzanie skoku
        if(input.Movement.Jump.IsPressed() && isJumping)
        {
            ContinueJump();
        }

        // Pusteny skok a vypnutie animacie
        if (input.Movement.Jump.WasReleasedThisFrame())
        {
            isJumping = false;
        }

        // Zvysim gravitaciu pri pade
        ApplyJumpPhysics();

        // Ak mam jetpack a pouzijem ho, zapni zvuk jetpacku
        if(hasJetpack && input.Movement.Jetpack.WasPressedThisFrame())
        {
            audioScript.AudioSource.PlayOneShot(audioScript.jetpackSound);
        }
        else if (hasJetpack && input.Movement.Jetpack.WasReleasedThisFrame())
        {
            audioScript.AudioSource.Stop();
        }
    }

    private void LateUpdate()
    {
        // Ohnem hraca do jednej alebo druhej strany ( = lepsi vizual pohybu )
        float targetTilt = moveInput.x * maxTilt;
        Vector3 targetRotation = new Vector3(0, 0, targetTilt);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(targetRotation), tiltSpeed * Time.deltaTime);
    }

    private void FixedUpdate()
    {
        // Pouzijem pohyb
        Move();

        // Sledujem orientaciu hraca podla pohybu
        FacingDirection();

        // Pouzijem jetpack
        UseJetpack();
    }

    private void Move()
    {
        // Cielova rychlost v smere X a Y (z vector2 vstupu)
        Vector2 targetVelocity = moveInput * moveSpeed;

        // Rozdiel medzi aktualnou a cielovou rychlostou
        Vector2 velocityDiff = targetVelocity - rb.linearVelocity;

        // Podla toho, ci sa hrac hybe alebo stoji, volim zrychlenie - brzdenie
        float accelRate = (moveInput.magnitude > 0.01f) ? acceleration : decelleration;

        // Sila, ktoru pridam
        Vector2 force = velocityDiff * accelRate;

        // Aplikujem silu do maxSpeed limitu
        if (rb.linearVelocity.magnitude < maxSpeed || Vector2.Dot(rb.linearVelocity, moveInput) < 0)
        {
            rb.AddForce(force);
        }
    }

    private void StartJump()
    {
        isJumping = true;
        jumpHoldTimer = 0f;

        // Zakladny odraz od zeme
        Vector2 vel = rb.linearVelocity;
        vel.y = jumpVelocity;
        rb.linearVelocity = vel;
    }

    private void ContinueJump()
    {
        jumpHoldTimer += Time.deltaTime;

        // Ak drzim space dlhsie ako povoluje limit, skoncim skok
        if(jumpHoldTimer > maxHoldTime)
        {
            isJumping = false;
            return;
        }
        
        // Pridanie mensej rychlosti pri drzani skoku hore
        Vector2 vel = rb.linearVelocity;
        vel.y = jumpVelocity + jumpHoldVelocity;
        rb.linearVelocity = vel;
    }

    private void ApplyJumpPhysics()
    {
        // Ak skocim alebo mam stlaceny skok a dosiahnem urcitu vysku, pridam gravitaciu
        if(rb.linearVelocity.y < 0)
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (fallMultiplier - 1) * Time.deltaTime;
        }
        else if(rb.linearVelocity.y > 0 && !input.Movement.Jump.IsPressed())
        {
            rb.linearVelocity += Vector2.up * Physics2D.gravity.y * (lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    private bool isGrounded()
    {
        if (groundCheck == null) return true; // fallback ak neni groundcheck

        // Kontrolujem ci som na zemi 
        return Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, groundLayer);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Ak spadnem do fall limitu, vratim sa na startovaciu polohu a zahram zvuk
        if (other.CompareTag("Fall"))
        {
            rb.transform.position = startPos;
            audioScript.AudioSource.PlayOneShot(audioScript.failSound, 0.25f);
        }

        // Ak zoberiem jetpack, mám jetpack, object sa vymaze a zahra zvuk
        if (other.gameObject.CompareTag("Jetpack"))
        {
            hasJetpack = true;
            gameManager.jetpackBar.fillAmount = 1;
            Destroy(other.gameObject);
            audioScript.AudioSource.PlayOneShot(audioScript.fuelPickupSound);
        }

        // Ak trafim "CheckPoint" hra zaznamena polohu checkpointu, zapise ako aktualnu startovaciu polohu po pade a zahra zvuk
        if (other.CompareTag("Save"))
        {
            startPos = other.transform.position;
            Destroy(other.gameObject);
            audioScript.AudioSource.PlayOneShot(audioScript.energyPickupSound, 0.35f);
        }

        // Ak zoberiem "money", object sa vymaze, pripocita sa mi skore a zahram zvuk
        if (other.gameObject.CompareTag("Money"))
        {
            gameManager.AddMoneyScore(100);
            Destroy(other.gameObject);
            audioScript.AudioSource.PlayOneShot(audioScript.moneySound, 0.25f);
        }

        // ak mam aspon 1500 " dolarov " a skocimm do end triggeru, zapnem finalnu scenku 
        if (gameManager.score > 1500 && other.gameObject.CompareTag("End"))
        {
            SceneManager.LoadScene("End Scene", LoadSceneMode.Single);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            audioScript.AudioSource.PlayOneShot(audioScript.fallSound);
        }
    }

    private void UseJetpack()
    {
        // Variable pre bar paliva ( + - )
        float flighTime = 2.5f;
        float useFuel = 1f / flighTime;
        float fillFuel = 0.5f;

        // Ak vlastnim a použijem jetpack, mozem letiet len do urcitej vysky pocitanej od aktualnej polohy Y 
        // + Zapnem "Motor" 
        // A ak som vyssie ako maximalna povolena vyska lietania jetpackom, padnem na zem
        // + Vypnem "Motor"
        if (hasJetpack && input.Movement.Jetpack.IsPressed() && transform.position.y < groundY + jetPackMaxY)
        {
            rb.AddForce(Vector2.up * jetPackForce, ForceMode2D.Impulse);
            gameManager.jetpackBar.fillAmount -= useFuel * Time.deltaTime;
            motorStart.SetActive(true);
        }
        else if (hasJetpack && transform.position.y > groundY + jetPackMaxY)
        {
            motorStart.SetActive(false);
        }

        // Ak mam jetpack a som na zemi, zaznamenam aktualnu polohu Y podla groundChecku
        if (hasJetpack && isGrounded())
        {
            groundY = transform.position.y;
            gameManager.jetpackBar.fillAmount += fillFuel;
            motorStart.SetActive(false);
        }
    }
    
    private void FacingDirection()
    {
        // Ak je input vacsi nez 0, teda doprava, postava mieri doprava
        // A ak je input mensi nez 0, teda dolava, postava mieri dolava
        if (moveInput.x > 0f)
        {
            transform.localScale = playerScale;
        }
        else if (moveInput.x < 0f)
        {
            transform.localScale = new Vector3(-1f, transform.localScale.y, transform.localScale.z);
        }
    }
}
