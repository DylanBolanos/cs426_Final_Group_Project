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
            Debug.Log("⏱ Charging... Time: " + contactTime);

            if (contactTime >= 3f && currentZone.HasCapacity()) // need 3 seconds for charging
            {
                filled = true;
                currentZone.UseOneCharge();
                Debug.Log("✅ Glass filled!");
            }
            // else
            // {
            //     if (inZone) Debug.Log("🧪 In zone but already filled or no currentZone");
            // }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chemical_zone"))
        {
            currentZone = other.GetComponent<Chemical_zone>();
            inZone = true;
            contactTime = 0f;
            Debug.Log("Entered Chemical Zone");

            // if (currentZone != null)
            // {
            //     Debug.Log("Zone capacity: " + currentZone.currentCapacity);
            // }
            // else
            // {
            //     Debug.LogWarning("ChemicalZone component NOT FOUND!");
            // }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chemical_zone"))
        {
            currentZone = null;
            inZone = false;
            contactTime = 0f;
            Debug.Log("🚪 Exited Chemical Zone");
        }
    }
}
