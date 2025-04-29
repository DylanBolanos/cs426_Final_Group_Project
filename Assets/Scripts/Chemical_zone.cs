using UnityEngine;

public class Chemical_zone : MonoBehaviour
{
    public int maxCapacity = 3;
    public int currentCapacity = 3;
    public ChemicalType type = ChemicalType.Acid;  // 🔥 이 Zone은 Acid zone!

    public bool HasCapacity()
    {
        return currentCapacity > 0;
    }

    public void UseOneCharge()
    {
        if (currentCapacity > 0)
        {
            currentCapacity--;
            Debug.Log("Acid zone is used. Remaining: " + currentCapacity);
        }
    }

    public void Refill()
    {
        currentCapacity = maxCapacity;
    }
}
