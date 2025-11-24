using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class FirstLevelHandler : MonoBehaviour
{
    private Animation countDownAnim;

    private void Start()
    {
        countDownAnim = FindAnyObjectByType<Animation>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            countDownAnim.Play();
            StartCoroutine(LoadMainLevel(10f));
        }
    }

    private IEnumerator LoadMainLevel(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("Main Level", LoadSceneMode.Single);
    }
}
