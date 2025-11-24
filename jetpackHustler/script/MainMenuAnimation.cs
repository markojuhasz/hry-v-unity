using UnityEngine;

public class MainMenuAnimation : MonoBehaviour
{
    [SerializeField] private Animation anim;

    private void Start()
    {
        anim = GetComponent<Animation>();
    }

    private void Update()
    {
        anim.Play();
    }
}
