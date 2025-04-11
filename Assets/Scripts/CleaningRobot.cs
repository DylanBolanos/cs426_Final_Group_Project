using UnityEngine;
using UnityEngine.AI;

public class CleaningRobot : MonoBehaviour
{
    enum State { Wandering, Idle, MovingToGlass, CleaningGlass }
    private State currentState;

    private NavMeshAgent _agent;

    GameObject holdGlass;
    Vector3 holdPatrolPoint;

    [SerializeField] private float speed = 2f;
    [SerializeField] private float wanderRadius = 10f;

    private Vector3 targetPosition;
    // [SerializeField] private string brokenGlassTag;
    [SerializeField] private string brokenGlassTag = "BrokenGlass"; 
    private Animator animator;
    

    private Bounds platformBounds;

    void Start(){
        _agent = GetComponent<NavMeshAgent>();
        currentState = State.Wandering;
        animator = GetComponent<Animator>();

        // Set Moving Range, that is main Platform
        GameObject platform = GameObject.FindGameObjectWithTag("Main_platform");
        if (platform != null){
            Collider platformCollider = platform.GetComponent<Collider>();
            if (platformCollider != null){
                platformBounds = platformCollider.bounds;
            }
        }
        FindNewDestination();

        animator.SetBool("Walking", true);
    }

    void Update(){
        switch (currentState)
        {
            case State.Wandering:
                Patrol();
                animator.SetBool("Walking", true);
                animator.SetBool("Cleaning",false);
                SearchForGlass();
                break;

            case State.MovingToGlass:
                MoveToGlass();
                break;

            case State.CleaningGlass:
                CleaningGlass();
                break;
        }
    }

    void SearchForGlass(){
        _agent.isStopped = false;
        holdGlass = GameObject.FindWithTag(brokenGlassTag);
        {
            if (holdGlass != null)
            {
                currentState = State.MovingToGlass;
                Debug.Log("Found Glass sending to movement"); 
            }
        }
    }


    void MoveToGlass(){
        _agent.SetDestination(holdGlass.transform.position);
        if (Vector3.Distance(transform.position, holdGlass.transform.position) <= 1f){
            currentState = State.CleaningGlass;
            _agent.isStopped = true;
        }
    }

    void Patrol(){
        _agent.isStopped = false;
        _agent.SetDestination(holdPatrolPoint);
        if (Vector3.Distance(transform.position, targetPosition) <= 1f)
        {
            holdPatrolPoint = FindNewDestination();
        }
    }

    Vector3 FindNewDestination(){
        Vector3 targetPos = new Vector3();
        bool found = false;
        int attempts = 10;
  
        while (!found && attempts-- > 0)
        {
            Vector3 randomDirection = Random.insideUnitSphere * wanderRadius;
            randomDirection += transform.position;

            if(NavMesh.SamplePosition(randomDirection, out NavMeshHit hit, wanderRadius, NavMesh.AllAreas))
            {
                targetPos = hit.position;
                NavMeshPath path = new NavMeshPath();
                if (_agent.CalculatePath(targetPos, path) && path.status == NavMeshPathStatus.PathComplete)
                {
                    found = true;
                }
            }
        }
        return targetPos;
    }

    private float timer = 5;
    void CleaningGlass(){
        animator.SetBool("Cleaning",true);
        timer -= Time.deltaTime;
        if(timer <= 0){
            animator.SetBool("Cleaning",false);
            timer = 5;
            currentState = State.Wandering;
            Destroy(holdGlass);
        }
    }
}
