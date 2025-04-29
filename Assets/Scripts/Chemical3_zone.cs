using UnityEngine;

public class Chemical3_zone : Chemical_zone
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        type = ChemicalType.CaCO3;
        maxCapacity = 3;
        currentCapacity = 3;
    }
}
