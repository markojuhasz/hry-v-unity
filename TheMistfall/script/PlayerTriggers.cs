using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerTriggers : MonoBehaviour
{
    // audio lebo .. HMM , Tak to este neumim 
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip yell;
    [SerializeField] private AudioClip spookyDoor;
    [SerializeField] private AudioClip metalRattle;
    [SerializeField] private AudioClip raven;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private AudioClip struggleSound;
    [SerializeField] private AudioClip jumpScare1;
    [SerializeField] private AudioClip jumpScare2;
    [SerializeField] private AudioClip jumpScare3;

    // struggleSound coolDown
    private const float cooldown = 1f;
    private float lastPlayedTime = -999;

    public GameManager gameManager;

    // pre zvuky lebo to este neviem
    private bool bookPickedUp;
    private bool hasBook;
    private bool isBookOpened;
    private bool yellWasPlaying = false;
    private bool spookyDoorWasPlaying = false;
    private bool metalRattleWasPlaying = false;
    private bool ravenWasPlaying = false;
    private bool jumpScare1WasPlaying = false;
    private bool jumpScare2WasPlaying = false;
    private bool jumpScare3WasPlaying = false;

    // hrac v nebezpecenstve 
    private const float stressAmount = 0.005f;

    private void Update()
    {
        // ak som zobral knihu a zavrem ju prvy krat, nahodim dalsi quest text a začne counter
        if (bookPickedUp && !isBookOpened)
        {
            gameManager.CountTime();
            gameManager.findObjectsQuest.SetActive(true);
        }

        // mozem prezerat knihu 
        if (hasBook && isBookOpened && Input.GetKeyDown(KeyCode.T))
        {
            gameManager.bookImage.SetActive(false);
            isBookOpened = false;
        }
        else if (hasBook && !isBookOpened && Input.GetKeyDown(KeyCode.T))
        {
            gameManager.bookImage.SetActive(true);
            isBookOpened = true;
        }

        // ak mam plny stres, umieram
        if(gameManager.panel.fillAmount == 0)
        {
            gameManager.Killed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // jump scare pri ohnisku
        if (!yellWasPlaying && other.CompareTag("OhniskoSound"))
        {
            audioSource.PlayOneShot(yell, 1f);
            yellWasPlaying = true;
        }

        // jump scare pri katedrale
        if (!spookyDoorWasPlaying && other.CompareTag("Katedrala"))
        {
            audioSource.PlayOneShot(spookyDoor, 1f);
            spookyDoorWasPlaying = true;
        }

        // jump scare pri kaplnke
        if (!metalRattleWasPlaying && other.CompareTag("Chapel"))
        {
            audioSource.PlayOneShot(metalRattle, 1f);
            metalRattleWasPlaying = true;
        }

        // zvuk vrán pri prvych hroboch
        if (!ravenWasPlaying && other.CompareTag("Hroby2Vrany"))
        {
            audioSource.PlayOneShot(raven, 1f);
            ravenWasPlaying = true;
        }

        // ak narazim do knihy, vypnem cas aby to hrac precital 
        if (other.CompareTag("Book"))
        {
            bookPickedUp = true;
            hasBook = true;
            gameManager.quest1Text.SetActive(false);
            gameManager.bookImage.SetActive(true);
            isBookOpened = true;
            Destroy(other.gameObject);
            audioSource.PlayOneShot(pickupSound, 1f);
        }

        // jumpScare
        if (!jumpScare1WasPlaying && other.CompareTag("jumpScare"))
        {
            audioSource.PlayOneShot(jumpScare1, 0.8f);
            jumpScare1WasPlaying = true;
        }
        
        if (!jumpScare2WasPlaying && other.CompareTag("jumpScare2"))
        {
            audioSource.PlayOneShot(jumpScare2, 0.8f);
            jumpScare2WasPlaying = true;
        }

        if(!jumpScare3WasPlaying && other.CompareTag("jumpScare3"))
        {
            audioSource.PlayOneShot(jumpScare3, 0.8f);
            jumpScare3WasPlaying = true;
        }

    }

    // ak ma napadne enemak
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.collider.CompareTag("Enemy"))
        {
            gameManager.panel.fillAmount -= stressAmount;

            Camera.main.GetComponent<PlayerView>().Shake(0.5f, 0.2f);

            if(Time.time - lastPlayedTime > cooldown)
            {
                audioSource.PlayOneShot(struggleSound, 0.25f);
                lastPlayedTime = Time.time;
            }
        }
    }
}
