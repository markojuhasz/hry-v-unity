using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void OnStartButton()
    {
        SceneManager.LoadScene("SampleScene", LoadSceneMode.Single);
    }

    public void OnExitButton()
    {
        Application.Quit();
    }
}
