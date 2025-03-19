using UnityEngine;

public class Experiment : MonoBehaviour, IInteractable
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    
    
    public void Interact()
    {
        PlayerMovement player = GameObject.FindWithTag("Player").GetComponent<PlayerMovement>();
        if (player.heldGlass != null)
        {
            Debug.Log("Experiment has been activated");
            //PerformExperiment(); // Small game during interacting with Experiment.
        }
        else
        {
            Debug.Log("Need Glass");
        }
    }
    void Start()
    {
    }
}
