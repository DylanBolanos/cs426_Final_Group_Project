using UnityEngine;
using TMPro;

public class Glass : MonoBehaviour, IInteractable
{
    public TextMeshPro chemical_name;
    public bool HCI_filled = false;
    public bool NaCl_filled = false;
    public bool NaOH_filled = false;
    public bool CaCO3_filled = false;
    public bool CaO_filled = false;
    public bool Co2_filled = false;
    public bool LiquidNaCl_filled = false;

    private float contactTime = 0f;

    private bool inZone = false;
    private bool inchemZone2 = false;
    private bool inchemZone3 = false;

    private bool inEndZone = false;

    private Chemical_zone chemical1;
    private Chemical2_zon chemical2;
    private Chemical3_zone chemical3;

    // private gameEndArea endZone;

    private MeshRenderer meshRenderer;

    public Material baseMaterial;
    public Material filledMaterial;
    public Material NaClFilledMaterial;
    public Material NaOHFilledMaterial;
    public Material CaCO3FilledMaterial;
    public Material CaOFilledMaterial;
    public Material Co2FilledMaterial;
    public Material LiquidNaCl_filledMaterial;

    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
        if (chemical_name != null)
        {
            chemical_name.text = "Empty";
        }
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

        if (inZone && !HCI_filled && chemical1 != null)
        {
            contactTime += Time.deltaTime;
            if (contactTime >= 5f && chemical1.HasCapacity())
            {
                ApplyChemicalEffect(chemical1.type);
                chemical1.UseOneCharge();
                Debug.Log("Glass filled from Acid zone!");
                UpdateMaterial();
            }
        }
        else if (inchemZone2 && !HCI_filled && chemical2 != null && !NaCl_filled)
        {
            contactTime += Time.deltaTime;
            if (contactTime >= 5f && chemical2.HasCapacity())
            {
                ApplyChemicalEffect(chemical2.type);
                chemical2.UseOneCharge();
                Debug.Log("Glass filled from NaOH zone!");
                UpdateMaterial();
            }
        }else if(inchemZone3 && !HCI_filled && chemical3 != null && !NaCl_filled && !CaCO3_filled){
            contactTime += Time.deltaTime;
            if (contactTime >= 5f && chemical3.HasCapacity())
            {
                ApplyChemicalEffect(chemical3.type);
                chemical3.UseOneCharge();
                Debug.Log("Glass filled from CaCO3 zone!");
                UpdateMaterial();
            }
        }
        else if(inEndZone){
            //impliment win
        }

    }

    private void ApplyChemicalEffect(ChemicalType type)
    {
        if (type == ChemicalType.Acid)
        {
            HCI_filled = true;
            NaOH_filled = false;
            NaCl_filled = false;
        }
        else if (type == ChemicalType.NaOH)
        {
            HCI_filled = false;
            NaOH_filled = true;
            NaCl_filled = false;
        }else if(type == ChemicalType.CaCO3){
             HCI_filled = false;
            NaOH_filled = false;
            NaCl_filled = false;
            CaCO3_filled = true;
        }
    }

    public void UpdateMaterial()
    {
        if (meshRenderer == null) return;

        Material[] materials = meshRenderer.materials;

        if (NaCl_filled)
        {
            materials[0] = NaClFilledMaterial;
            UpdateStatusText("NaCl");
        }
        else if (NaOH_filled)
        {
            materials[0] = NaOHFilledMaterial;
            UpdateStatusText("NaOH");
        }else if (HCI_filled)
        {
            materials[0] = filledMaterial;
            UpdateStatusText("HCl");
        }else if (CaCO3_filled){
            materials[0] = CaCO3FilledMaterial;
            UpdateStatusText("CaCO₃");
        }else if (CaO_filled){
            materials[0] = CaOFilledMaterial;
            UpdateStatusText("CaO");
        }
        else if (Co2_filled)
        {
            materials[0] = Co2FilledMaterial;
            UpdateStatusText("CO₂");
        }else if (LiquidNaCl_filled)
        {
            materials[0] = LiquidNaCl_filledMaterial;
            UpdateStatusText("Liquid NaCl");
        }
        else
        {
            materials[0] = baseMaterial;
            UpdateStatusText("Empty");
        }
        // else
        // {
        //     materials[0] = HCI_filled ? filledMaterial : baseMaterial;
        // }

        meshRenderer.materials = materials;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Chemical_zone"))
        {
            chemical1 = other.GetComponent<Chemical_zone>();
            inZone = true;
        }
        else if (other.CompareTag("Chemical_zone2"))
        {
            chemical2 = other.GetComponent<Chemical2_zon>();
            inchemZone2 = true;
        }else if(other.CompareTag("Chemical_zone3")){
            chemical3 = other.GetComponent<Chemical3_zone>();
            inchemZone3 = true;
        }
        // else if (other.CompareTag("gameEndArea")){        
        //     inEndZone = true;
        // }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Chemical_zone"))
        {
            chemical1 = null;
            inZone = false;
        }
        else if (other.CompareTag("Chemical_zone2"))
        {
            chemical2 = null;
            inchemZone2 = false;
        }
        else if(other.CompareTag("Chemical_zone3")){
            chemical3 = null;
            inchemZone3 = false;
        }
        // else if(other.CompareTag("gameEndArea")){
        //     inEndZone = false;
        // }
    }
    private void UpdateStatusText(string label)
    {
        if (chemical_name != null)
        {
            chemical_name.text = label;
        }
}
}

