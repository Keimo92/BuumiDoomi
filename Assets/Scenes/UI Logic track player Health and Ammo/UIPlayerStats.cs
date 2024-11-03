using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIPlayerStats : MonoBehaviour
{
    PlayerWeapon playerWeapon;
    PlayerEntity Player;

    [SerializeField] private TextMeshProUGUI Health;
    [SerializeField] private TextMeshProUGUI AmmoText;

    private void Start()
    {
        Player = FindFirstObjectByType<PlayerEntity>();
        playerWeapon = FindFirstObjectByType<PlayerWeapon>();
        
    }

    private void Update()
    {
        if (Player != null)
        {
            Health.text = "Health = " + Player.GetHealth().ToString();
            AmmoText.text = "Ammo = " + playerWeapon.AmmoCount.ToString();
        }
    }
}

