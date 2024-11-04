using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing", menuName = "Health/HealthData")]
public class HealthPackData : ScriptableObject
{
    public int healingAmount;
}
