using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FANDR : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 50;
    [SerializeField] private float floatamplitude = 2.0f;
    [SerializeField] private float floatfrequency = 2.0f;

    private Vector3 startpos;

    void Start()
    {
        startpos = transform.position;
    }

    
    void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime);
        Vector3 tempPos = startpos;
        tempPos.y = Mathf.Sin(Time.fixedTime * Mathf.PI * floatfrequency) * floatamplitude;
        transform.position = tempPos;
    }
}
