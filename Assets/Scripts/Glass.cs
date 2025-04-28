using UnityEngine;

public class Glass : MonoBehaviour, IInteractable
{
    public bool filled = false;
    public bool advanced_filled = false;
    private float contactTime = 0f;
    private bool inZone = false;
    private Chemical_zone currentZone;
    private MeshRenderer meshRenderer;

    public Material baseMaterial;
    public Material filledMaterial;
    public Material advancedFilledMaterial;
    

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
       // UpdateMaterial();
    }

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
        if (transform.parent != null) return;
        if (inZone && !filled && currentZone != null)
        {
            contactTime += Time.deltaTime;
            Debug.Log("Zone will fill Flask within 5sec" + contactTime);

            if (contactTime >= 5f && currentZone.HasCapacity()) // need 5 seconds for charging
            {
                filled = true;
                currentZone.UseOneCharge();
                Debug.Log("Glass filled!!");
                UpdateMaterial();
            }
        }
    }
    public void UpdateMaterial()
    {
        if (meshRenderer == null) return;

        Material[] materials = meshRenderer.materials;

        if (advanced_filled)
        {
            materials[0] = advancedFilledMaterial;
        }
        else
        {
            materials[0] = filled ? filledMaterial : baseMaterial;
        }

        meshRenderer.materials = materials;
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
