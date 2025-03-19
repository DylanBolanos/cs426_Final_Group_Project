using UnityEngine;

public class Glass : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Interact()
    {
        PlayerMovement player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        if (player.heldGlass == null)
        {
            player.heldGlass = this.gameObject;
            transform.SetParent(player.transform);
            transform.localPosition = new Vector3(0.5f, 0.3f, 1f); // when the player is holding the glass
            transform.localRotation = Quaternion.identity;
            GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("Player Holds a glass");
        }
    }
    
    void Start()
    {
    }
}
