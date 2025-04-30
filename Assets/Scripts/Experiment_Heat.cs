using UnityEngine;

public class Experiment_Heat : MonoBehaviour
{
    public float detectRadius = 5f;

    private PlayerMovement player;
    private bool playerInRange = false;

    void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>();
            if (player == null)
                return;
        }

        Vector3 playerPos = player.transform.position;
        Vector3 experimentPos = transform.position;
        playerPos.y = 0f;
        experimentPos.y = 0f;

        float distance = Vector3.Distance(playerPos, experimentPos);

        if (distance <= detectRadius)
        {
            playerInRange = true;
        }
        else
        {
            playerInRange = false;
        }

        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (player == null || player.heldGlass == null)
        {
            Debug.LogWarning("Plz, Hold a Flask to start experiment.");
            return;
        }

        Glass heldGlass = player.heldGlass.GetComponent<Glass>();
        if (heldGlass == null)
            return;

        if (heldGlass.CaCO3_filled)
        {
            Glass emptyNearby = FindNearbyEmptyGlass();
            if (emptyNearby != null)
            {
                float chance = Random.Range(0f, 1f);

                if (chance <= 0.99f)
                {
                    heldGlass.CaCO3_filled = false;
                    heldGlass.CaO_filled = true;
                    heldGlass.UpdateMaterial();

                    emptyNearby.Co2_filled = true;
                    emptyNearby.UpdateMaterial();

                    Debug.Log("Experiment Success: CaCO3 -> CaO + CO2");
                }
                else
                {
                    Debug.LogWarning("Experiment Failed! Both Flasks destroyed!");
                    Destroy(player.heldGlass);
                    player.heldGlass = null;
                    Destroy(emptyNearby.gameObject);
                }
            }
            else
            {
                Debug.LogWarning("No empty flask nearby to collect CO2!");
            }
        }

        else if (heldGlass.NaCl_filled && !heldGlass.LiquidNaCl_filled)
        {
            heldGlass.NaCl_filled = false;
            heldGlass.LiquidNaCl_filled = true;
            heldGlass.UpdateMaterial();

            Debug.Log("Experiment Success: NaCl -> Liquid NaCl");
        }

        else
        {
            Debug.LogWarning("This Flask is not valid for this experiment.");
        }
    }


    // private Glass FindNearbyEmptyGlass()
    // {
    //     Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius);

    //     foreach (Collider collider in colliders)
    //     {
    //         Glass glass = collider.GetComponent<Glass>();
    //         if (glass != null &&
    //             !glass.HCI_filled && !glass.NaCl_filled && glass.NaOH_filled &&
    //             !glass.CaCO3_filled && !glass.CaO_filled && !glass.Co2_filled)
    //         {
    //             return glass;
    //         }
    //     }

    //     return null;
    // }
    private Glass FindNearbyEmptyGlass()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius);

        foreach (Collider collider in colliders)
        {
            Glass glass = collider.GetComponent<Glass>();
            if (glass != null &&
                !glass.HCI_filled && !glass.NaCl_filled && !glass.NaOH_filled &&
                !glass.CaCO3_filled && !glass.CaO_filled && !glass.Co2_filled &&
                !glass.LiquidNaCl_filled)
            {
                return glass;
            }
        }

        return null;
    }


    private Glass FindNearbyNaClGlass()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius);

        foreach (Collider collider in colliders)
        {
            Glass glass = collider.GetComponent<Glass>();
            if (glass != null && glass.NaCl_filled && !glass.LiquidNaCl_filled)
            {
                return glass;
            }
        }

        return null;
    }
}
