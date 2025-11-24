using UnityEngine;

public class PlayerControll : MonoBehaviour
{
    private PlayerInput _input;
    private Rigidbody2D rb;
    public Vector2 startPos; 

    [SerializeField] private bool isPlayerOne = true;
    [SerializeField] private float moveSpeed = 5.0f;

    public void Start()
    {
        startPos = transform.position;
    }

    private void Awake()
    {
        _input = new PlayerInput();
        rb= GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void Update()
    {
        float moveInput;

        if (isPlayerOne)
        {
            moveInput = _input.Player.MoveP1.ReadValue<float>();
        }
        else
        {
            moveInput = _input.Player.MoveP2.ReadValue<float>();
        }

        Vector3 move = new Vector3(0, moveInput * moveSpeed * Time.deltaTime, 0);
        transform.Translate(move);

        float clampedY = Mathf.Clamp(transform.position.y, -3.5f, 3.5f);
        transform.position = new Vector3(transform.position.x, clampedY, transform.position.z);

    }
}
