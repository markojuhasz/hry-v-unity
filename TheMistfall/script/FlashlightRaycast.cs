using UnityEngine;

public class FlashlightRaycast : MonoBehaviour
{
    [SerializeField] private Light flashLight;
    [SerializeField] private float range = 20f;
    [SerializeField] private LayerMask hitLayers; // pre AI 
    private AIVisibility[] allAI;

    private void Start()
    {
        // nacitam vsetky AI len raz 
        allAI = FindObjectsByType<AIVisibility>(FindObjectsSortMode.None);
    }

    private void Update()
    {
        // AI su neviditelne a mozu sa hybat 
        foreach (var ai in allAI)
            ai.SetVisible(false);

        if (!flashLight.enabled)
            return;

        Ray ray = new(flashLight.transform.position, flashLight.transform.forward);
        RaycastHit hit;

        // ak trafim AI, prestanu sa hybat a budu viditelne 
        if(Physics.Raycast(ray, out hit, range, hitLayers))
        {
            AIVisibility ai = hit.collider.GetComponent<AIVisibility>();

            if(ai != null)
            {
                ai.SetVisible(true);
            }
        }
    }
}
