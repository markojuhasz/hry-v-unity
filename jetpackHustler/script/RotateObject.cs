using UnityEngine;

public class RotateObject : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 90f;
    [SerializeField] private float maxAngle = 180f;
    private bool rotatingForward = true;
    private float currentAngle = 90f;

    private void Update()
    {
        float step = rotationSpeed * Time.deltaTime;

        if (rotatingForward)
        {
            currentAngle += step;
            if (currentAngle >= maxAngle)
            {
                currentAngle = maxAngle;
                rotatingForward = false;
            }
        }
        else
        {
            currentAngle -= step;
            if (currentAngle <= 0f)
            {
                currentAngle = 0f;
                rotatingForward = true;
            }
        }

        transform.localRotation = Quaternion.Euler(0f, 0f, -currentAngle);
    }
}
