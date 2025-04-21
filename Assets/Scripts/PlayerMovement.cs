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
    }

    private void TryPickUpGlass()
    {
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
        heldGlass.transform.SetParent(null);
        heldGlass.GetComponent<Rigidbody>().isKinematic = false;
        heldGlass = null;
        Debug.Log("Player Dropped off Glass");
        // if (heldGlass != null)
        // {
        //     heldGlass.transform.SetParent(null);
        //     Rigidbody rb = heldGlass.GetComponent<Rigidbody>();
        //     if (rb != null)
        //     {
        //         rb.isKinematic = false;
        //     }
        //     heldGlass = null;
        //     Debug.Log("Player Dropped off Glass");
        // }
    }
    
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
}
