using UnityEngine;
using UnityEngine.AI;

public class NavigationScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public Transform player;
    private NavMeshAgent agent;
    public float stoppingDistance = 1.0f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        agent.stoppingDistance = stoppingDistance;
    }

    // Update is called once per frame
    void Update()
    {
        // agent.destination = player.position;

        float distance = Vector3.Distance(transform.position, player.position);

        if (distance > stoppingDistance)
        {
            agent.destination = player.position;
        }
        else
        {
            agent.ResetPath(); // Stop moving if within the stopping distance
        }
    }
}
