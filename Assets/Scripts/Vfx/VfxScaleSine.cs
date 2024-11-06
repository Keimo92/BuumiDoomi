using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxScaleSine : MonoBehaviour
{
    [SerializeField] float speed;
    [SerializeField] float magnitude;

    [SerializeField] float minScale;

    Vector3 originalScale;

    private void Start()
    {
        originalScale = transform.localScale;
    }
    private void Update()
    {
        transform.localScale = originalScale * (minScale + Mathf.Abs(Mathf.Sin(Time.time*speed)) * magnitude);
    }
}
