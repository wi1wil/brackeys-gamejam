using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObesityScript : MonoBehaviour
{
    public int obesityType;
    public int eatenBiscuits;
    public int totalBiscuits;

    public void AddType()
    {
        eatenBiscuits++;
        totalBiscuits++;
        if (eatenBiscuits > 2)
        {
            obesityType++;
            eatenBiscuits--;
        }
        Debug.Log($"Obesity Type: {obesityType}");
    }
}
