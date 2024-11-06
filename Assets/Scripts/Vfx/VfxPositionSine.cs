using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VfxPositionSine : MonoBehaviour
{
    [SerializeField] Vector3 sineAxis;
    [SerializeField] float magnitude;
    [SerializeField] float speed;

    Vector3 originalPos;

    private void Start()
    {
        originalPos = transform.position;
    }
    private void Update()
    {
        transform.position += sineAxis.normalized * magnitude * Mathf.Sin(Time.time * speed) * Time.deltaTime;
    }
}
