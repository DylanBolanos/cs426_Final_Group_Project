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

    void Start()
    {
        currentLocation = door.transform.position.x; // Store original position
    }
    void Update()
    {
        if (leftSide) {
            if (opening)
            {
                // if (currentRot.y < openRot)
                // {
                    // door.transform.localEulerAngles = Vector3.Lerp(currentRot, new Vector3(currentRot.x, openRot, currentRot.z), speed * Time.deltaTime);

                if ( door.transform.position.x < currentLocation + 1f ) {

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
                    door.transform.Translate( right*speed*Time.deltaTime ); 
                }
            }
        }
    }

    public void ToggleDoor()
    {
        opening = !opening;
    }
}