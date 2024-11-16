using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxSpin : MonoBehaviour
{
    [SerializeField] float speed;
    private void Update()
    {
        transform.Rotate(new Vector3(0, Time.deltaTime * speed, 0));
    }
}
