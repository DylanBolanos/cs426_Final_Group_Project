using UnityEngine;

public class Glass : MonoBehaviour, IInteractable
{
    public bool filled = false;
    private float contactTime = 0f;
    private bool inZone = false;
    private Chemical_zone currentZone;

    public void Interact()
    {
        PlayerMovement player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();

        if (player.heldGlass == null)
        {
            player.heldGlass = this.gameObject;
            transform.SetParent(player.transform);
            transform.localPosition = new Vector3(0.5f, 0.3f, 1f);
            transform.localRotation = Quaternion.identity;
            GetComponent<Rigidbody>().isKinematic = true;
            Debug.Log("Player Holds a glass");
        }
    }

    void Update()
    {
        if (inZone && !filled && currentZone != null)
        {
            contactTime += Time.deltaTime;
            Debug.Log("Zone will fill Flask within 5sec" + contactTime);

            if (contactTime >= 3f && currentZone.HasCapacity()) // need 3 seconds for charging
            {
                filled = true;
                currentZone.UseOneCharge();
                Debug.Log("Glass filled!!");
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chemical_zone"))
        {
            currentZone = other.GetComponent<Chemical_zone>();
            inZone = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chemical_zone"))
        {
            currentZone = null;
            inZone = false;
        }
    }
}
