using UnityEngine;
using UnityEngine.AI;

public class AIVisibility : MonoBehaviour
{
    private MeshRenderer[] renderers;
    private NavMeshAgent agent;
    private AudioSource audioSource;

    private void Start()
    {
        renderers = GetComponentsInChildren<MeshRenderer>();

        agent = GetComponent<NavMeshAgent>();

        audioSource = GetComponent<AudioSource>();

        SetVisible(false);
    }

    public void SetVisible(bool state)
    {
        foreach (var r in renderers)
            r.enabled = state;

        if(agent != null)
            agent.isStopped = state; // ked svietim AI stoji 

        if(audioSource != null)
        {
            if(state) // AI viditelne, stoji 
            {
                if (audioSource.isPlaying)
                    audioSource.Stop();
            }

            if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }

    }
}
