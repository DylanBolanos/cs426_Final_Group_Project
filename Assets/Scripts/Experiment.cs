using UnityEngine;

public class Experiment : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject miniGameUI;
    public float detectRadius = 4f;

    private PlayerMovement player;
    private bool playerInRange = false;

    private void Update()
    {
        if (player == null)
        {
            player = FindObjectOfType<PlayerMovement>();
            if (player != null)
            {
                Debug.Log("[test] Experiment figured out the Player!");
            }
            else
            {
                return;
            }
        }

        // 거리 체크 (XZ 평면)
        Vector3 playerPos = player.transform.position;
        Vector3 experimentPos = transform.position;
        playerPos.y = 0f;
        experimentPos.y = 0f;

        float distance = Vector3.Distance(playerPos, experimentPos);

        if (distance <= detectRadius)
        {
            if (!playerInRange)
            {
                playerInRange = true;
                Debug.Log("[test] Player is in range of Experiment 1");
            }
        }
        else
        {
            if (playerInRange)
            {
                playerInRange = false;
                Debug.Log("[test] Player left the range of Experiment 1");
            }
        }

        // Press F for interact
        if (playerInRange && Input.GetKeyDown(KeyCode.F))
        {
            Interact();
        }
    }

    public void Interact()
    {
        if (player == null)
            return;

        if (player.heldGlass != null)
        {
            Glass glass = player.heldGlass.GetComponent<Glass>();
            if (glass != null && glass.filled)
            {
                Debug.Log("Flask is filled!!, Game is able to run!");

                if (miniGameUI != null)
                {
                    miniGameUI.SetActive(true);

                    MiniGameManager miniGameManager = miniGameUI.GetComponent<MiniGameManager>();
                    if (miniGameManager != null)
                    {
                        miniGameManager.SetTargetGlass(glass);
                    }
                }
            }
            else
            {
                Debug.LogWarning("Flask is empty, it is not able to run experiment");
            }
        }
        else
        {
            Debug.LogWarning("Please hold Flask, it is not able to run experiment");
        }
    }
}
