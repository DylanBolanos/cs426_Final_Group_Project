using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public GameObject door;
    public float openRot, closeRot, speed;
    public bool opening;
    public bool leftSide = true;
    private Vector3 left = Vector3.left;
    private Vector3 right = Vector3.right;
    private float currentLocation;
    public AudioSource audioSource;
    private bool audioPlaying = false;

    void Start()
    {
        currentLocation = door.transform.position.x; // Store original position
    }
    void Update()
    {
        if (leftSide) {
            Debug.Log("Sound: " + audioSource.isPlaying);

            if (opening)
            {
                // if (currentRot.y < openRot)
                // {
                    // door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRot, currentRot.z), speed * Time.deltaTime);
                if ( door.transform.position.x < currentLocation + 1f ) {
                    if (!audioPlaying) {
                        audioPlaying = true;
                        audioSource.Play();
                    }
                    door.transform.Translate( right*speed*Time.deltaTime ); 
                }
                // }
            }
            else
            {
                // if (currentRot.y > closeRot)
                // {
                //     door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, closeRot, currentRot.z), speed * Time.deltaTime);
                // }

                if ( door.transform.position.x > currentLocation ) {
                    if (!audioPlaying) {
                        audioPlaying = true;
                        audioSource.Play();
                    }
                    door.transform.Translate( left*speed*Time.deltaTime ); 
                }
            }
        } else {
            if (opening)
            {
                // if (currentRot.y < openRot)
                // {
                    // door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRot, currentRot.z), speed * Time.deltaTime);

                if ( door.transform.position.x > currentLocation - 1f ) {
                    if (!audioPlaying) {
                        audioPlaying = true;
                        audioSource.Play();
                    }

                    door.transform.Translate( left*speed*Time.deltaTime ); 
                }
                // }
            }
            else
            {
                // if (currentRot.y > closeRot)
                // {
                //     door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, closeRot, currentRot.z), speed * Time.deltaTime);
                // }

                if ( door.transform.position.x < currentLocation ) {
                    if (!audioPlaying) {
                        audioPlaying = true;
                        audioSource.Play();
                    }

                    door.transform.Translate( right*speed*Time.deltaTime ); 
                }
            }
        }

        if (!audioSource.isPlaying) {
            audioPlaying = false;
        }
    }

    public void ToggleDoor()
    {
        opening = !opening;
    }
}