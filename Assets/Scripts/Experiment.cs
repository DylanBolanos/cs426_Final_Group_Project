using UnityEngine;

public class Experiment : MonoBehaviour, IInteractable
{
    public float detectRadius = 2f;
    [SerializeField] private GameObject miniGameUI; // UI 오브젝트를 inspector에서 할당

    public void Interact()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectRadius);

        foreach (Collider collider in colliders)
        {
            Glass glass = collider.GetComponent<Glass>();
            if (glass != null && glass.filled)
            {
                Debug.Log("✔️ Filled glass detected. Activating MiniGame UI...");
                if (miniGameUI != null)
                {
                    miniGameUI.SetActive(true);
                }
                else
                {
                    Debug.LogWarning("⚠️ MiniGame UI is not assigned in the inspector.");
                }
                return;
            }
        }

        Debug.Log("❌ No filled glass nearby. Experiment not available.");
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }
}
