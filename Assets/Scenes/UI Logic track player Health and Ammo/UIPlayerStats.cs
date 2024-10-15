using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    Shooty Shooty;
    PlayerEntity Player;

    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private TextMeshProUGUI AmmoText;

    private void Start()
    {
        Player = FindFirstObjectByType<PlayerEntity>();
        Shooty = FindFirstObjectByType<Shooty>();
        
    }

    private void Update()
    {
        // Safely access the health value through the public property
        if (Player != null)
        {
            Health.text = "Health = " + Player.GetHealth().ToString();
            AmmoText.text = "Ammo = " + Shooty.AmmoCount.ToString();
        }
    }
}

