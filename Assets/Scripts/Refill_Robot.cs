using UnityEngine;

public class Refill_Robot : MonoBehaviour
{
    enum State { Wandering, MovingToZone }
    private State currentState;

    public float speed = 2f;
    public float wanderRadius = 10f;

    private Vector3 targetPosition;
    private Chemical_zone zoneToRefill;
    private Animator animator;

    private Bounds platformBounds;

    void Start()
    {
        currentState = State.Wandering;
        animator = GetComponent<Animator>();

        // Set Moving Range, that is main Platform
        GameObject platform = GameObject.FindGameObjectWithTag("Main_platform");
        if (platform != null)
        {
            Collider platformCollider = platform.GetComponent<Collider>();
            if (platformCollider != null)
            {
                platformBounds = platformCollider.bounds;
            }
        }
        FindNewDestination();

        animator.SetBool("walking", true);
        animator.SetBool("working", false);
    }

    void Update() // get started
    {
        switch (currentState)
        {
            case State.Wandering:
                Patrol();
                animator.SetBool("walking", true);
                animator.SetBool("working", false);
                SearchToRefill();
                break;

            case State.MovingToZone:
                MoveToZone();
                break;
        }
    }

    void SearchToRefill()
    {
        foreach (var zone in FindObjectsOfType<Chemical_zone>())
        {
            if (zone != null && zone.currentCapacity < 3)
            {
                zoneToRefill = zone;
                currentState = State.MovingToZone;
                Debug.Log("Found empty zone, Refil_Robot is heading there!"); // need to modify, between chemical, Flask, and etc
                break;
            }
        }
    }

    void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }


    void MoveToZone()
    {
        if(zoneToRefill == null){
            return;
        }

        animator.SetBool("walking", true);
        animator.SetBool("working", false);

        RotateTowards(zoneToRefill.transform.position);
        transform.position = Vector3.MoveTowards(transform.position, zoneToRefill.transform.position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, zoneToRefill.transform.position) < 1.5f)
        {
            animator.SetBool("walking", false);
            animator.SetBool("working", true);

            Debug.Log("Chemical zone is refilling by robot");
            zoneToRefill.Refill();
            zoneToRefill = null;
            
            Invoke(nameof(ResumePatrol), 5f); // refilling 
            
        }
    }

    void ResumePatrol() // restart walking
    {
        animator.SetBool("walking", true);
        animator.SetBool("working", false);
        FindNewDestination();
        currentState = State.Wandering;

        Debug.Log("Chemical zone is refilled by robot");
    }

    void Patrol()
    {
        RotateTowards(targetPosition);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);
        if (Vector3.Distance(transform.position, targetPosition) < 0.5f)
        {
            FindNewDestination();
        }
    }

    void FindNewDestination()
    {
        // Main_platform
        Vector3 randomPoint;
        int attempts = 0;
        do
        {
            float randX = Random.Range(platformBounds.min.x, platformBounds.max.x);
            float randZ = Random.Range(platformBounds.min.z, platformBounds.max.z);
            randomPoint = new Vector3(randX, transform.position.y, randZ);
            attempts++;
        } while (!platformBounds.Contains(randomPoint) && attempts < 10);

        targetPosition = randomPoint;
    }
}
