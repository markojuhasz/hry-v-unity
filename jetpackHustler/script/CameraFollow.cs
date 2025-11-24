using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public float smoothedSpeed = 0.05f;
    public Vector3 offset = new Vector3(0, 0, -10);

    // Maximalny offset kamery
    private float minX = 0;
    private float maxX = 290;
    private float minY = 0;
    private float maxY = 32;

    private void LateUpdate()
    {
        // Želaná pozícia kamery
        Vector3 desiredPosition = target.position + offset;

        // Maximálna a minimálna pozícia kamery 
        desiredPosition.x = Mathf.Clamp(desiredPosition.x, minX, maxX);
        desiredPosition.y = Mathf.Clamp(desiredPosition.y, minY, maxY);
        desiredPosition.z = offset.z;

        // Plynuly pohyb 
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothedSpeed);

        // pozicia kamery = plynulemu pohybu
        transform.position = smoothedPosition;
    }
}
