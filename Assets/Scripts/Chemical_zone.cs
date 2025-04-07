using UnityEngine;

public class Chemical_zone : MonoBehaviour
{
    public int maxCapacity = 3;
    public int currentCapacity = 3;

    public bool HasCapacity()
    {
        return currentCapacity > 0;
    }

    public void UseOneCharge()
    {
        if (currentCapacity > 0)
        {
            currentCapacity--;
            Debug.Log("Chemical zone is used. Remaining: " + currentCapacity);
        }
    }

    public void Refill()
    {
        currentCapacity = maxCapacity;
    }
}
