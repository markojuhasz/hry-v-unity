using UnityEngine;

public class PlayerView : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float speed = 120f;

    // Pozicia kamery 
    private Vector3 offset;

    // Horizontalny , vertikalny uhol 
    private float yaw;
    private float pitch;

    // Obmedzenie pitch 
    private const float minPitch = -90f;
    private const float maxPitch = 80f;

    // cam shake
    private Vector3 shakeOffset = Vector3.zero;
    private float shakeTimer = 0f;
    private float shakeMagnitude = 0f;

    // Zamknem kurzor 
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        offset = transform.position - target.position;

        // Pociatocne uhly
        Vector3 angles = transform.eulerAngles;
        pitch = angles.x;
        yaw = angles.y;
    }

    private void Update()
    {
        // Input 
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        yaw += mouseX * speed * Time.deltaTime;
        pitch -= mouseY * speed * Time.deltaTime;

        // Obmedzenie rotacie na osi X 
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        // Prepocet pozicie kamery
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);
        transform.SetPositionAndRotation(target.position + rotation * offset, rotation);
    }

    // vypocet na cameraShake 
    private void LateUpdate()
    {
        if(shakeTimer > 0f)
        {
            shakeTimer -= Time.deltaTime;
            shakeOffset = new Vector3(Random.Range(-1f, 1f) * shakeMagnitude, Random.Range(-1f, 1f) * shakeMagnitude, 0f);
            transform.position += shakeOffset;
        }
        else if(shakeOffset != Vector3.zero)
        {
            transform.position -= shakeOffset;
            shakeOffset = Vector3.zero;
        }
    }

    // cameraShake
    public void Shake(float duration, float magnitude)
    {
        shakeTimer = duration;
        shakeMagnitude = magnitude;
    }
}
