using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing", menuName = "ScriptableObjects/HealthPacks")]
public class HealthPackData : ScriptableObject
{
    public int healingAmount;
}
