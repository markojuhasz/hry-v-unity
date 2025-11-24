using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    private Vector3 shakeOffset = Vector3.zero;
    private float shakeTimer = 0f;
    private float shakeMagnitude = 0f;

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
            //vratim offset
            transform.position -= shakeOffset;
            shakeOffset = Vector3.zero;
        }
    }

    public void Shake(float duration, float magnitude)
    {
        shakeTimer = duration;
        shakeMagnitude = magnitude;
    }
}
