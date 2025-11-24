using System.Collections;
using UnityEngine;

public class BallMovement : MonoBehaviour
{
    public float startForce = 15f; //Sila odpalu
    private Rigidbody2D ballRb; //Rigidbody Variable
    private Vector2 startPosition; //Start pozicia
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();
        ballRb = GetComponent<Rigidbody2D>();
        ResetBall();
    }

    private void ResetBall()
    {
        //Stop lopty a presun na start
        ballRb.linearVelocity = Vector2.zero;
        transform.position = startPosition;

        StartCoroutine(LaunchAfterDelay());
    } 

    private IEnumerator LaunchAfterDelay()
    {
        yield return new WaitForSeconds(1f);
        LaunchBall();
    }

    private void LaunchBall()
    {
        float dirX = Random.value < 0.5f ? -1f : 1f;
        float dirY = Random.Range(-0.5f, 0.5f);

        Vector2 direction = new Vector2(dirX, dirY).normalized;
        ballRb.AddForce(direction * startForce, ForceMode2D.Impulse);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Left Goal"))
        {
            startForce += 0.5f;
            gameManager.AddScoreToPlayer(2);
            ResetBall();
        }
        if (collision.CompareTag("Right Goal"))
        {
            startForce += 0.15f;
            gameManager.AddScoreToPlayer(1);
            ResetBall();
        }
    }
}
