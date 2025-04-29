using UnityEngine;

public class Chemical2_zon : Chemical_zone
{
    void Start()
    {
        type = ChemicalType.NaOH;
        maxCapacity = 3;
        currentCapacity = 3;
    }
}
