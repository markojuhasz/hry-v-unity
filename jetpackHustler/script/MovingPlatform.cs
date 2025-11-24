using System.Collections;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("PLimit"))
        {
            StartCoroutine(ResetAfterDelay(2f));
        }
    }

    private IEnumerator ResetAfterDelay(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        //Reset platformy na povodnu poziciu
        transform.position = startPos;
    }
}
