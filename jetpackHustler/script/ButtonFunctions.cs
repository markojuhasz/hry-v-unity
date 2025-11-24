using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class ButtonFunctions : MonoBehaviour
{
    [SerializeField] private AudioSource mySound;
    [SerializeField] private AudioClip hoverSound;
    [SerializeField] private AudioClip clickSound;

    // Ked prejdem mysou na tlacitko, trigernem zvuk
    public void OnMouseHover()
    {
        mySound.PlayOneShot(hoverSound);
    }

    // Ked kliknem, trigernem zvuk
    public void OnMouseClick()
    {
        mySound.PlayOneShot(clickSound);
    }

    // Ked stlacim start, zapnem coroutine a po 1s nacitam novu scenu
    public void OnStartClick()
    {
        StartCoroutine(LoadNextScene(1f));
    }

    public void OnExitClick()
    {
        Application.Quit();
    }

    // Odpocet 
    public IEnumerator LoadNextScene(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        SceneManager.LoadScene("First Level", LoadSceneMode.Single);
    }
}
