using UnityEngine;

public class ShelfTrigger : MonoBehaviour
{
    public DoorController door;
    public AudioSource audioSource;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DoorController door = GetComponent<DoorController>();
            Debug.Log("Collide");
            if (door != null)
            {
                audioSource.Play();
                door.ToggleDoor();
            }
        }
    }
}
