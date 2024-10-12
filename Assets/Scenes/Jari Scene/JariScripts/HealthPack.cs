using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Healing", menuName = "ScriptableObjects/HealthPacks")]
public class HealthPack : ScriptableObject
{
    public string Health;
    public float HealingAmount;
}
