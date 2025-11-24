using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class Subtitles : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI subtitles;
    private float textSpeed = 120f;

    private void Awake()
    {
        StartCoroutine(LoadMainMenuScene(40f));
    }

    private void Update()
    {
        subtitles.transform.Translate(Vector2.up * textSpeed * Time.deltaTime);
    }

    // Nacitanie main menu po skonceni titulkov
    private IEnumerator LoadMainMenuScene(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        SceneManager.LoadScene("Main Menu", LoadSceneMode.Single);
    }
}
