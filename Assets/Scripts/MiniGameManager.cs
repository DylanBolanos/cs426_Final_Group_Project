using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class MiniGameManager : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI resultText;
    public TextMeshProUGUI rangeText;
    public float scrollSpeed = 0.5f;

    private float minRange;
    private float maxRange;
    private bool scrollingRight = true;
    private int successCount = 0;
    private int targetSuccess = 3;
    private bool gameCleared = false;
    private Glass targetGlass;
    private PlayerMovement playerMovement;

    void Start()
    {
        GenerateNewRange();
    }

    public void SetTargetGlass(Glass glass)
    {
        targetGlass = glass;
        playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.canPickUp = false;
        }
    }

    void Update()
    {
        if (gameCleared) return;

        float value = slider.value;
        value += (scrollingRight ? scrollSpeed : -scrollSpeed) * Time.deltaTime;

        if (value >= 1f)
        {
            value = 1f;
            scrollingRight = false;
        }
        else if (value <= 0f)
        {
            value = 0f;
            scrollingRight = true;
        }

        slider.value = value;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            CheckMatch();
        }
    }

    void GenerateNewRange()
    {
        int baseValue = Random.Range(10, 70);
        minRange = baseValue / 100f;
        maxRange = (baseValue + 20) / 100f;
        rangeText.text = $"Match: {baseValue} - {baseValue + 20}";
    }

    void CheckMatch()
    {
        float value = slider.value;

        if (value >= minRange && value <= maxRange)
        {
            successCount++;
            resultText.text = $"Success {successCount}/{targetSuccess}";

            if (successCount >= targetSuccess)
{
                resultText.text = "Game Cleared!";
                gameCleared = true;

                if (targetGlass != null)
                {
                    targetGlass.HCI_filled = false;
                    targetGlass.advanced_filled = true;
                    targetGlass.UpdateMaterial();
                }

                StartCoroutine(CloseAfterDelay());
                return;
            }

            GenerateNewRange();
        }
        else
        {
            resultText.text = "Missed! Try again!";
            successCount = 0;
            GenerateNewRange();
        }
    }

    IEnumerator CloseAfterDelay()
    {
        yield return new WaitForSeconds(2f);
        ClearNearbyNaOHFlasks();
        if (playerMovement != null)
        {
            playerMovement.canPickUp = true;
        }
        gameObject.SetActive(false); // or use SceneManager.LoadScene if needed
    }

    private void ClearNearbyNaOHFlasks()
    {
        float checkRadius = 5f;

        // Collider[] colliders = Physics.OverlapSphere(transform.position, checkRadius);
        Collider[] colliders = Physics.OverlapSphere(playerMovement.transform.position, checkRadius);

        foreach (Collider collider in colliders)
        {
            Glass glass = collider.GetComponent<Glass>();

            if (glass != null && glass != targetGlass && glass.NaOH_filled)
            {
                glass.NaOH_filled = false;
                glass.UpdateMaterial();
                Debug.Log("Nearby NaOH Flask reset to normal.");
            }
            Debug.Log("11111111111111111");
        }
    }


}
