using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public int collected = 0; 
    public GameObject pressEText;
    private GameObject itemInRange = null; // predmet pri hracovi

    public GameManager manager;

    [SerializeField] private AudioSource pAudio;
    [SerializeField] private AudioClip pickupSound;

    private void Start()
    {
        if(pressEText != null)
        {
            pressEText.SetActive(false);
        }
    }

    private void Update()
    {
        // stlacenie E
        if(itemInRange != null && Input.GetKeyDown(KeyCode.E))
        {
            MarkAndAddCollected();
            Destroy(itemInRange);
            pAudio.PlayOneShot(pickupSound, 1f);
            itemInRange = null;
            pressEText.SetActive(false);
        }

        // ak vyzbieram všetky predmety ukončím hru 
        if(collected == 5)
        {
            manager.FinishedGame();
        }
    }

    // trigger predmetu
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            itemInRange = other.gameObject;
            pressEText.SetActive(true);
        }
    }

    // ked odidem z triggeru von 
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Item"))
        {
            itemInRange = null;
            pressEText.SetActive(false);
        }
    }

    private void MarkAndAddCollected()
    {
        if (collected < manager.crosses.Length)
        {
            manager.crosses[collected].SetActive(true);
        }
        collected++;
    }
}
