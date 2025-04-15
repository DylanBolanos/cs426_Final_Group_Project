using UnityEngine;

public class Refill_Robot : MonoBehaviour
{
    enum State { Wandering, MovingToZone }
    private State currentState;

    public float speed = 2f;
    public float wanderRadius = 10f;

    private Vector3 targetPosition;
    private Chemical_zone zoneToRefill;
    private Flask_zone flaskZoneTarget;
    private Animator animator;

    private Bounds platformBounds;

    private AudioSource walkingAudio;
    private AudioSource workingAudio;

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

        // get audio sources in robot
        AudioSource[] audios = GetComponents<AudioSource>();
        if (audios.Length >= 2)
        {
            walkingAudio = audios[0];
            workingAudio = audios[1];

            walkingAudio.loop = true;
            workingAudio.loop = true;
        }
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
                PlayWalkingSound();
                break;

            case State.MovingToZone:
                MoveToZone();
                // PlayWalkingSound();
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
                flaskZoneTarget = null;
                currentState = State.MovingToZone;
                Debug.Log("Found Chemical zone, Refil_Robot is heading there!"); // need to modify, between chemical, Flask, and etc
                return;
            }
        }

        foreach (var zone in FindObjectsOfType<Flask_zone>())
        {
            var flaskZoneType = zone.GetType();
            var capacityField = flaskZoneType.GetField("currentCapacity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            int currentFlaskCount = (int)capacityField.GetValue(zone);

            if (currentFlaskCount < 3)
            {
                flaskZoneTarget = zone;
                zoneToRefill = null;
                currentState = State.MovingToZone;
                Debug.Log("Found Flask zone is empty, Refill_Robot is heading there!");
                return;
            }
        }
        if (zoneToRefill == null && flaskZoneTarget == null)
        {
            currentState = State.Wandering;
            Debug.Log("No zone needs refill. Continue wandering.");
        }

    }

    void MoveToZone()
    {
        Vector3? target = null;

        if (zoneToRefill != null)
            target = zoneToRefill.transform.position;
        else if (flaskZoneTarget != null)
            target = flaskZoneTarget.transform.position;

        if (!target.HasValue)
        {
            return;
        }

        animator.SetBool("walking", true);
        animator.SetBool("working", false);

        RotateTowards(target.Value);
        transform.position = Vector3.MoveTowards(transform.position, target.Value, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, target.Value) >= 1.5f)
        {
            animator.SetBool("walking", true);
            animator.SetBool("working", false);
            PlayWalkingSound();
        }
        else
        {
            animator.SetBool("walking", false);
            animator.SetBool("working", true);
            PlayWorkingSound();
            
            if (zoneToRefill != null)
            {
                zoneToRefill.Refill();
                zoneToRefill = null;
                Debug.Log("Refilled chemical zone.");
            }

            if (flaskZoneTarget != null)
            {
                flaskZoneTarget.RefillDispenser();
                flaskZoneTarget = null;
                Debug.Log("Refilled flask zone.");
            }

            Invoke(nameof(ResumePatrol), 5f);
        }

    }

    void ResumePatrol() // restart walking
    {
        animator.SetBool("walking", true);
        animator.SetBool("working", false);
        FindNewDestination();
        currentState = State.Wandering;
        PlayWalkingSound();
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

    void RotateTowards(Vector3 target)
    {
        Vector3 direction = (target - transform.position).normalized;
        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void PlayWalkingSound()
    {
        if (walkingAudio != null && !walkingAudio.isPlaying)
            walkingAudio.Play();

        if (workingAudio != null && workingAudio.isPlaying)
            workingAudio.Stop();
    }

    void PlayWorkingSound()
    {
        if (workingAudio != null && !workingAudio.isPlaying)
            workingAudio.Play();

        if (walkingAudio != null && walkingAudio.isPlaying)
            walkingAudio.Stop();
    }
}
