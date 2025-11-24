using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float visionRange = 10f;
    public float visionAngle = 60f;
    public float wanderRadius = 8f;
    public float timeBetweenWanders = 3f;

    private NavMeshAgent agent;
    private float wanderTimer;

    private enum State { Wander, Chase, Return };
    private State currentState = State.Wander;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        bool canSeePlayer = CanSeePlayer();

        if (canSeePlayer)
        {
            currentState = State.Chase;
        }
        else if(currentState == State.Chase)
        {
            currentState = State.Wander;
        }

        if(currentState == State.Wander)
        {
            Wander();
        }
        else if( currentState == State.Chase)
        {
            ChasePlayer();
        }
    }

    // hlada, chodi
    void Wander()
    {
        wanderTimer += Time.deltaTime;

        if(wanderTimer >= timeBetweenWanders)
        {
            Vector3 newPos = RandomNavSphere(transform.position, wanderRadius);

            agent.SetDestination(newPos);
            wanderTimer = 0f;
        }
    }

    // zbadal hraca tak ide po nom
    void ChasePlayer()
    {
        agent.SetDestination(player.position);
    }

    // vidi hraca ??
    bool CanSeePlayer()
    {
        Vector3 dirToPlayer = (player.position - transform.position).normalized;    

        // Kontrola uhla 
        if(Vector3.Angle(transform.forward, dirToPlayer) > visionAngle)
            return false;

        // Kontrola vzdialenosti 
        if(Vector3.Distance(transform.position, player.position) > visionRange)
            return false;


        // Raycast - ci hraca nieco nezakryva 
        if(Physics.Raycast(transform.position + Vector3.up * 1.5f, dirToPlayer, out RaycastHit hit, visionRange))
            if(hit.transform == player)
            {
                return true;
            }

            return false;
    }

    // pohyb
    public static Vector3 RandomNavSphere(Vector3 origin, float dist)
    {
        Vector3 randomDir = Random.insideUnitSphere * dist;
        randomDir.y = 0;

        randomDir += origin;

        NavMesh.SamplePosition(randomDir, out NavMeshHit navHit, dist, NavMesh.AllAreas);

        return navHit.position;
    }
}
