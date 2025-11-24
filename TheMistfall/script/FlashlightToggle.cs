using UnityEngine;

public class Flashlight : MonoBehaviour
{
    public Light flashlight;
    [SerializeField] private AudioSource pAudio;
    [SerializeField] private AudioClip flashlightSound;
    [SerializeField] private KeyCode toggleKey = KeyCode.F;

    private void Update()
    {
        if (Input.GetKeyDown(toggleKey))
        {
            flashlight.enabled = !flashlight.enabled;
            pAudio.PlayOneShot(flashlightSound, 1f);
        }
    }
}
