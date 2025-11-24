using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    // inicializovanie objektov pouzitych v inom skripte
    public GameObject quest1Text;
    public GameObject actionText;
    public GameObject bookImage;
    public GameObject findObjectsQuest;
    public GameObject[] crosses;
    public Image panel;
    public GameObject timeEnded;
    public GameObject killed;
    public GameObject finished;
    public TextMeshProUGUI timer;

    // pauza hry cez game manager
    [SerializeField] private GameObject pauseMenu;

    private bool isGamePaused = false;

    private void Start()
    {
        quest1Text.SetActive(true);
    }

    private void Update()
    {
        // ak nemam pauznutu hru a stlacim ESC 
        if(!isGamePaused && Input.GetKeyDown(KeyCode.Escape))
        {
            PauseGame();
        }
    }

    // rychli reset hry cez scenu
    public void ResetGame()
    {
        // nacitam scenu 
        Scene current = SceneManager.GetActiveScene();

        SceneManager.LoadScene(current.name, LoadSceneMode.Single);
    }

    // pre tlacitko ukoncenia 
    public void EndGame()
    {
        Application.Quit();
    }

    // ak nestihnem do 15 minut 
    public void TimeEnded()
    {
        // zapnem kurzor
        Cursor.lockState = CursorLockMode.Confined;

        // nastavim timeEnded image
        timeEnded.SetActive(true);
    }

    // ak ma zabiju entity 
    public void Killed()
    {
        // zapnem kurzor
        Cursor.lockState= CursorLockMode.Confined;

        // nastavim kill image
        killed.SetActive(true);
    }

    // ked vyzbieram vsetky predmety 
    public void FinishedGame()
    {
        // zap kurzor
        Cursor.lockState = CursorLockMode.Confined;

        // nastavim finished image
        finished.SetActive(true);
    }

    // ked pauznem hru ESC 
    private void PauseGame()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.Confined;
        pauseMenu.SetActive(true);
        isGamePaused = true;
    }

    // pre tlacitko Pokracovat
    public void ContinueButton()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);
        isGamePaused = false;
    }

    // casovac 
    public void CountTime()
    {
        StartCoroutine(CountDownCoroutine());
    }

    // pocitanie casu 
    private IEnumerator CountDownCoroutine()
    {
        int currentHour = 23;
        int currentMinute = 45;

        // pokial nie je 00:00
        while(currentHour != 0 || currentMinute != 0)
        {
            // zobrazim cas 
            timer.text = string.Format("{0:D2}:{1:D2}", currentHour, currentMinute);

            // cakam 1 sekundu
            yield return new WaitForSeconds(60f);

            // inkrementujem cas 
            currentMinute++; 
            if(currentMinute >= 60)
            {
                currentMinute = 0;
                currentHour++;
                if(currentHour >= 24)
                    currentHour = 0;
            }
        }
        // vysledny cas - 00:00
        timer.text = "00:00";
        StopCoroutine(CountDownCoroutine());
        TimeEnded();
    }
}
