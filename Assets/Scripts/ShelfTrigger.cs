using UnityEngine;

public class ShelfTrigger : MonoBehaviour
{
    public DoorController door;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            DoorController door = GetComponent<DoorController>();
            Debug.Log("Collide");
            if (door != null)
            {
                door.ToggleDoor();
            }
        }
    }
}
