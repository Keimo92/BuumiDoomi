using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunFollow : MonoBehaviour
{
    public Transform Gunpoint;


    private void Update()
    {
        transform.position = Gunpoint.position;
    }
}
