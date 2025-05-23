using UnityEngine;

public class Flask_zone : MonoBehaviour
{
    public GameObject flaskPrefab;
    public Transform spawnPoint;
    public int maxCapacity = 3;
    private int currentCapacity;

    void Start()
    {
        currentCapacity = maxCapacity;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && currentCapacity > 0)
        {
            DispenseFlask();
        }
    }

    void DispenseFlask()
    {
        Instantiate(flaskPrefab, spawnPoint.position, spawnPoint.rotation);
        currentCapacity--;
        Debug.Log("Flask provided! Remaining: " + currentCapacity);

        AudioSource audio = GetComponent<AudioSource>();
        if (audio != null && !audio.isPlaying)
        {
            audio.Play();
        }
    }

    public void RefillDispenser()
    {
        currentCapacity = maxCapacity;
        Debug.Log("Dispenser is refilling");
    }
}
