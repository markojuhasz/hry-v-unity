using UnityEngine;

public class PlayerAudioScript : MonoBehaviour
{
    public AudioSource AudioSource;
    public AudioClip jumpSound;
    public AudioClip fallSound;
    public AudioClip fuelPickupSound;
    public AudioClip energyPickupSound;
    public AudioClip walkSound;
    public AudioClip jetpackSound;
    public AudioClip failSound;
    public AudioClip moneySound;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }
}
