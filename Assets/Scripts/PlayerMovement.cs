using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 10f;
    public float rotationSpeed = 100f;
    public float force = 300f;
    public Transform cameraTransform;
    public float mouseSensitivity = 2.0f;
    private float cameraRotationX = 0f;
    public static bool isused = false;
    // glass
    private IInteractable nearbyInteractable = null;
    private float interactionDistance = 0f;
    private Transform nearbyObjectTransform; 

    public GameObject heldGlass = null;
    private Animator anim;
    private DoorController currentDoor = null;
    public GameObject brokenGlassPrefab;
    public bool canPickUp = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    Rigidbody rb;
    Transform t;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        t = GetComponent<Transform>();
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        cameraRotationX -= mouseY;
        cameraRotationX = Mathf.Clamp(cameraRotationX, -90f, 90f);
        cameraTransform.localRotation = Quaternion.Euler(cameraRotationX, 0f, 0f);
        
        if (Input.GetKey(KeyCode.W))
            rb.linearVelocity += this.transform.forward * speed * Time.deltaTime;
        else if (Input.GetKey(KeyCode.S))
            rb.linearVelocity -= this.transform.forward * speed * Time.deltaTime;

        //rotation
        if (Input.GetKey(KeyCode.D))
            t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
        else if (Input.GetKey(KeyCode.A))
            t.rotation *= Quaternion.Euler(0, - rotationSpeed * Time.deltaTime, 0);
    

        if(Input.GetKeyDown(KeyCode.F)){
            TryInteract();
            // anim.SetTrigger("PickingUp");
        }
        if(Input.GetKeyDown(KeyCode.G)){
            DropGlass();
        }
        bool isRunning = Input.GetKey(KeyCode.LeftShift);

        if (heldGlass != null && heldGlass.scene.IsValid() && isRunning)
        {
            BreakGlassInHand();
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Shelf"))
        {
            Debug.Log("Shelf tag detected on collision!");
            currentDoor = collision.gameObject.GetComponent<DoorController>();
            if (currentDoor != null)
            {
                Debug.Log("DoorController found, setting opening to true.");
                currentDoor.opening = true;
            }
            else
            {
                Debug.LogWarning("DoorController component not found on the door object!");
            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision exit with: " + collision.gameObject.name);
        if (collision.gameObject.CompareTag("Shelf"))
        {
            DoorController door = collision.gameObject.GetComponent<DoorController>();
            if (door != null && door == currentDoor)
            {
                Debug.Log("Exiting current door collision, setting opening to false.");
                door.opening = false;
                currentDoor = null;
            }
            else
            {
                Debug.LogWarning("DoorController not found on exit or mismatched door.");
            }
        }
    }


    private void TryPickUpGlass()
    {

        if (!canPickUp) return;
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 2f);
        foreach (Collider collider in hitColliders)
        {
            // Debug.Log("Nearby: " + collider.name);
            if (collider.CompareTag("Glass"))
            {
                heldGlass = collider.gameObject;
                heldGlass.transform.SetParent(this.transform);
                heldGlass.transform.localPosition = new Vector3(0.0f, 0.5f, 1f); // Flask's location when player is holding
                heldGlass.GetComponent<Rigidbody>().isKinematic = true;
                Debug.Log("Player Picked up glass");
                return;
            }
        }
    }
    private void DropGlass()
    {
        if (heldGlass == null)
        {
            Debug.LogWarning("DropGlass() called, but no glass is currently held!");
            return;
        }

        heldGlass.transform.SetParent(null);

        Rigidbody rb = heldGlass.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.isKinematic = false;
        }

        heldGlass = null;
        Debug.Log("Player Dropped off Glass");
    }

    // private void DropGlass()
    // {
    //     heldGlass.transform.SetParent(null);
    //     heldGlass.GetComponent<Rigidbody>().isKinematic = false;
    //     heldGlass = null;
    //     Debug.Log("Player Dropped off Glass");
        
    // }
    
    private void TryInteract(){
        if (nearbyInteractable != null)
        {
            float distanceToObject = Vector3.Distance(transform.position, nearbyObjectTransform.position);// distance between player and object
            if (distanceToObject <= interactionDistance)
            {
                if (nearbyObjectTransform.CompareTag("Experiment") && heldGlass == null){
                    Debug.Log("Need to Hold a Glass");
                }
                else{
                    nearbyInteractable.Interact();
                }
            }
        }
        else
        {
            TryPickUpGlass(); // it is definitely work
        }
    }

    private void BreakGlassInHand()
    {
        if (heldGlass == null) return;
        AudioSource audioSource = heldGlass.GetComponent<AudioSource>();
        if (audioSource != null && audioSource.clip != null)
        {
            audioSource.PlayOneShot(audioSource.clip);
        }

        GameObject brokenGlass = Instantiate(brokenGlassPrefab, heldGlass.transform.position, Quaternion.identity);
        brokenGlass.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        Vector3 spawnPosition = heldGlass.transform.position;
        spawnPosition.z += 0.355f; 
        Destroy(heldGlass, 0.5f);

        heldGlass = null;

        Debug.Log("Flask is broken!");
    }

}
